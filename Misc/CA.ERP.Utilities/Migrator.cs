using CA.ERP.Common.Types;
using CA.ERP.DataAccess;
using CA.ERP.DataAccess.Entities;
using CA.ERP.Domain.Core;
using CA.ERP.Domain.Helpers;
using CA.ERP.Utilities.MoneyConvertionStrategies;
using CA.ERP.Utilities.PrevDataModel;
using CA.Identity.Data;
using CA.Identity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using New = CA.ERP.DataAccess.Entities;

namespace CA.ERP.Utilities
{
    public class Migrator
    {

        public static async Task Migrate(IServiceProvider services)
        {
            CitiAppDatabaseContext citiAppDatabaseContext = services.GetRequiredService<CitiAppDatabaseContext>();

            using (var scope = services.CreateScope())
            {
                using (var newDbContext = scope.ServiceProvider.GetRequiredService<CADataContext>())
                {
                    newDbContext.Database.EnsureDeleted();
                    newDbContext.Database.Migrate();
                }

                using (var newDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>())
                {
                    newDbContext.Database.EnsureDeleted();
                    newDbContext.Database.Migrate();
                }
            }



            Console.WriteLine("Migrating suppliers");

            //migrate supplier
            var oldSuppliers = await citiAppDatabaseContext.Suppliers.ToListAsync();

            var newSuppliers = new List<New.Supplier>();
            //add deleted suplier holder
            New.Supplier newDeletedSupplier = new New.Supplier()
            {
                Name = "Deleted",
                Address = "Deleted",
                ContactPerson = "Deleted"
            };
            newSuppliers.Add(newDeletedSupplier);
            foreach (var supplier in oldSuppliers)
            {
                New.Supplier newSupplier = new New.Supplier();
                newSupplier.Name = supplier.SName;
                newSupplier.Address = supplier.Address;
                newSupplier.ContactPerson = supplier.Contact;

                newSuppliers.Add(newSupplier);
            }
            //save suppliers
            using (var scope = services.CreateScope())
            {
                using (var newDbContext = scope.ServiceProvider.GetRequiredService<CADataContext>())
                {
                    newDbContext.Suppliers.AddRange(newSuppliers);
                    await newDbContext.SaveChangesAsync();
                }
            }


            Console.WriteLine("Migrating branch");
            //migrate brands

            var oldBrands = await citiAppDatabaseContext.Brands.ToListAsync();
            var newBrands = new List<New.Brand>();
            var newDeletedBrand = new New.Brand()
            {
                Name = "Deleted",
                Description = "Deleted",
            };
            newBrands.Add(newDeletedBrand);
            foreach (var gbrand in oldBrands.GroupBy(b => b.BrandName))
            {
                New.Brand newBrand = new New.Brand();
                newBrand.Name = gbrand.Key;
                foreach (var oldSupplierBrand in gbrand)
                {
                    var oldSupplier = oldSuppliers.Where(s => oldSupplierBrand.SupIdno == s.SupIdno).FirstOrDefault();
                    if (oldSupplier != null)
                    {
                        New.SupplierBrand newSupplierBrand = new SupplierBrand()
                        {
                            Brand = newBrand,
                            SupplierId = newSuppliers.Where(s => s.Name == oldSupplier.SName).Select(s => s.Id).FirstOrDefault()
                        };

                        newBrand.SupplierBrands.Add(newSupplierBrand);
                    }

                }
                newBrands.Add(newBrand);

            }

            using (var scope = services.CreateScope())
            {
                using (var newDbContext = scope.ServiceProvider.GetRequiredService<CADataContext>())
                {
                    newDbContext.Brands.AddRange(newBrands);
                    await newDbContext.SaveChangesAsync();
                }
            }

            Console.WriteLine("Migrating master products");
            //migrate model to masterproducts 
            var oldModels = await citiAppDatabaseContext.Models.ToListAsync();
            //var productLists = await citiAppDatabaseContext.ProductLists.ToListAsync();

            var newMasterProducts = new List<New.MasterProduct>();

            //delete products placeholder
            var newDeletedMasterProduct = new MasterProduct()
            {
                Model = "Deleted",
                Description = "Deleted",
                ProductStatus = 0,
                BrandId = newDeletedBrand.Id,
            };
            newMasterProducts.Add(newDeletedMasterProduct);

            foreach (var gModel in oldModels.GroupBy(m => m.ModelName))
            {
                New.MasterProduct newMasterProduct = new New.MasterProduct();
                newMasterProduct.Model = gModel.Key;
                newMasterProduct.Description = null;

                foreach (var model in gModel.OrderBy(m => m.BrandId))
                {
                    var oldBrand = oldBrands.FirstOrDefault(b => b.BrandId == model.BrandId);
                    if (oldBrand != null)
                    {
                        var newBrand = newBrands.FirstOrDefault(b => oldBrand.BrandName == b.Name);
                        if (newBrand != null)
                        {
                            newMasterProduct.BrandId = newBrand.Id;
                        }
                    }
                }

                newMasterProducts.Add(newMasterProduct);
            }

            using (var scope = services.CreateScope())
            {
                using (var newDbContext = scope.ServiceProvider.GetRequiredService<CADataContext>())
                {
                    newDbContext.MasterProducts.AddRange(newMasterProducts);
                    await newDbContext.SaveChangesAsync();
                }
            }


            Console.WriteLine("Migrating supplier products");
            //migrate productList to supplierMasterProduct
            var productLists = await citiAppDatabaseContext.ProductLists.ToListAsync();
            var newSupplierMasterProducts = new List<New.SupplierMasterProduct>();
            foreach (var productList in productLists)
            {
                New.SupplierMasterProduct newSupplierMasterProduct = new SupplierMasterProduct();
                if (decimal.TryParse(productList.Price, out decimal costPrice))
                {
                    newSupplierMasterProduct.CostPrice = costPrice;

                }

                var oldBrand = oldBrands.FirstOrDefault(b => b.BrandId == productList.BrandId);
                var newBrand = newBrands.FirstOrDefault(b => b.Name == oldBrand.BrandName);
                var oldModel = oldModels.FirstOrDefault(m => m.ModelId == productList.ModelId);
                if (newBrand != null && oldModel != null)
                {
                    var newMasterProduct = newMasterProducts.FirstOrDefault(mp => mp.Model == oldModel.ModelName && mp.BrandId == newBrand.Id);
                    if (newMasterProduct != null)
                    {
                        newSupplierMasterProduct.MasterProductId = newMasterProduct.Id;
                    }
                }

                var oldSupplier = oldSuppliers.FirstOrDefault(s => s.SupIdno == productList.SupIdno);
                if (oldSupplier != null)
                {
                    var newSupplier = newSuppliers.FirstOrDefault(s => s.Name == oldSupplier.SName);
                    if (newSupplier != null)
                    {
                        newSupplierMasterProduct.SupplierId = newSupplier.Id;
                    }
                }

                if (newSupplierMasterProduct.MasterProductId != Guid.Empty && newSupplierMasterProduct.SupplierId != Guid.Empty)
                {
                    var duplicate = newSupplierMasterProducts.FirstOrDefault(smp => smp.MasterProductId == newSupplierMasterProduct.MasterProductId && smp.SupplierId == newSupplierMasterProduct.SupplierId);
                    if (duplicate != null)
                    {
                        newSupplierMasterProducts.Remove(duplicate);
                    }
                    newSupplierMasterProducts.Add(newSupplierMasterProduct);
                }

            }

            using (var scope = services.CreateScope())
            {
                using (var newDbContext = scope.ServiceProvider.GetRequiredService<CADataContext>())
                {
                    newDbContext.SupplierMasterProducts.AddRange(newSupplierMasterProducts);
                    await newDbContext.SaveChangesAsync();
                }
            }

            Console.WriteLine("Migrating branches");

            var oldBranches = await citiAppDatabaseContext.Branches.ToListAsync();
            var newBranches = new List<New.Branch>();

            //add deleted placeholder
            var newDeletedBranch = new New.Branch();
            newDeletedBranch.Name = "Deleted";
            newDeletedBranch.BranchNo = 0;
            newDeletedBranch.Code = "D";
            newDeletedBranch.Address = "Deleted";
            newDeletedBranch.Contact = "Deleted";

            newBranches.Add(newDeletedBranch);

            foreach (var oldBranchG in oldBranches.GroupBy(b => b.BranchName))
            {
                int i = 1;
                foreach (var oldBranch in oldBranchG)
                {
                    New.Branch newBranch = new New.Branch();
                    newBranch.Name = oldBranch.BranchName;
                    newBranch.BranchNo = BranchNumberComverter(oldBranch.BranchNo);
                    newBranch.Code = oldBranch.BranchCode;
                    newBranch.Address = oldBranch.BAddress;
                    newBranch.Contact = oldBranch.BContactNo;
                    newBranches.Add(newBranch);
                    if (i == 2)
                    {
                        newBranch.Name += $" - {i}";
                    }
                    i++;
                }
            }

            using (var scope = services.CreateScope())
            {
                using (var newDbContext = scope.ServiceProvider.GetRequiredService<CADataContext>())
                {
                    newDbContext.Branches.AddRange(newBranches);
                    await newDbContext.SaveChangesAsync();
                }
            }
            List<ApplicationUser> newUsers;
            using (var scope = services.CreateScope())
            {
                newUsers = await scope.ServiceProvider.GetRequiredService<UserMigrator>().Migrate(newBranches);
                await scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().SaveChangesAsync();

            }



            Console.WriteLine("Migrating purchase orders");
            var oldPurchaseOrders = citiAppDatabaseContext.PurchaseOrders.Include(po => po.PoDetails).ToList();
            var newPurchaseOrders = new List<New.PurchaseOrder>();
            var rgmUser = newUsers.FirstOrDefault(u => u.UserName == "rgm");

            Parallel.ForEach(oldPurchaseOrders, oldPurchaseOder =>
            {
                New.PurchaseOrder newPurchaseOrder = new New.PurchaseOrder();
                newPurchaseOrder.Barcode = oldPurchaseOder.PoId;
                newPurchaseOrder.DeliveryDate = oldPurchaseOder.DeliveryDate ?? DateTime.Now;
                newPurchaseOrder.TotalCostPrice = decimal.Parse(oldPurchaseOder.TotalAmount);
                newPurchaseOrder.Status = Status.Active;
                newPurchaseOrder.ApprovedById = Guid.Parse( rgmUser.Id);

                var oldSupplier = oldSuppliers.FirstOrDefault(s => s.SupIdno == oldPurchaseOder.SupIdno);
                if (oldSupplier != null)
                {
                    var newSupplier = newSuppliers.FirstOrDefault(s => s.Name == oldSupplier.SName);
                    if (newSupplier != null)
                    {
                        newPurchaseOrder.SupplierId = newSupplier.Id;
                    }
                }
                if (newPurchaseOrder.SupplierId == Guid.Empty)
                {
                    newPurchaseOrder.SupplierId = newDeletedSupplier.Id;
                }
                var newBranch = newBranches.FirstOrDefault(c => c.Address.Contains(oldPurchaseOder.BranchNo));
                if (newBranch == null)
                {
                    string branchNo = GetBranchForPO(oldPurchaseOder.BranchNo);
                    newBranch = newBranches.FirstOrDefault(c => c.BranchNo == BranchNumberComverter(branchNo));


                }
                if (newBranch != null)
                {
                    newPurchaseOrder.DestinationBranchId = newBranch.Id;
                }
                else
                {
                    throw new Exception("No branch found");
                }

                oldPurchaseOder.NewPurchaseOrder = newPurchaseOrder;

                foreach (var oldPurchaseOrderItem in oldPurchaseOder.PoDetails)
                {
                    New.PurchaseOrderItem newPurchaseOrderItem = new PurchaseOrderItem();
                    newPurchaseOrderItem.OrderedQuantity = decimal.Parse(oldPurchaseOrderItem.OrderedQty);
                    newPurchaseOrderItem.FreeQuantity = decimal.Parse(oldPurchaseOrderItem.FreeQty);
                    newPurchaseOrderItem.TotalQuantity = decimal.Parse(oldPurchaseOrderItem.TotalQty);
                    newPurchaseOrderItem.CostPrice = StringToMoneyConverter(oldPurchaseOrderItem.Cost);
                    newPurchaseOrderItem.Discount = StringToMoneyConverter(oldPurchaseOrderItem.Discount);
                    newPurchaseOrderItem.TotalCostPrice = StringToMoneyConverter(oldPurchaseOrderItem.TotalCost);
                    newPurchaseOrderItem.DeliveredQuantity = newPurchaseOrderItem.OrderedQuantity - decimal.Parse(oldPurchaseOrderItem.RemainingQty ?? "0");
                    newPurchaseOrderItem.PurchaseOrderItemStatus = Domain.PurchaseOrderAgg.PurchaseOrderItemStatus.Completed;

                    var newMasterProduct = newMasterProducts.FirstOrDefault(m => m.Model == oldPurchaseOrderItem.Model);
                    if (newMasterProduct != null)
                    {
                        newPurchaseOrderItem.MasterProductId = newMasterProduct.Id;
                    }
                    else
                    {
                        newPurchaseOrderItem.MasterProductId = newDeletedMasterProduct.Id;
                    }

                    oldPurchaseOrderItem.NewPurchaseOrderItem = newPurchaseOrderItem;

                    newPurchaseOrderItem.PurchaseOrderId = newPurchaseOrder.Id;

                    newPurchaseOrder.PurchaseOrderItems.Add(newPurchaseOrderItem);
                }
                if (newPurchaseOrder.Id == Guid.Empty)
                {
                    throw new Exception("asd");
                }
                newPurchaseOrders.Add(newPurchaseOrder);
                Console.WriteLine("Added PO");
            });


            Console.WriteLine("Saving PO");
            using (var scope = services.CreateScope())
            {
                using (var newDbContext = scope.ServiceProvider.GetRequiredService<CADataContext>())
                {
                    newDbContext.PurchaseOrders.AddRange(newPurchaseOrders);
                    await newDbContext.SaveChangesAsync();
                }
            }

            //stocks
            var oldStocks = citiAppDatabaseContext.Products.ToList();
            var oldStocksGroup = oldStocks.GroupBy(s => s.DeliveryNo);
            var oldPurchaseOrderItems = citiAppDatabaseContext.PoDetails.ToList();
            var newStockReceives = new List<New.StockReceive>();
            var newStocks = new ConcurrentBag<New.Stock>();
            Parallel.ForEach(oldStocksGroup, oldStockG =>
            {
                var firstOldStockG = oldStockG.FirstOrDefault();


                New.StockReceive newStockReceive = new New.StockReceive();
                newStockReceive.DateReceived = firstOldStockG.DateReceived ?? DateTime.Now;
                newStockReceive.DeliveryReference = firstOldStockG.DeliveryNo;

                //purchaseOrder
                var newPurchaseOrder = oldPurchaseOrders.FirstOrDefault(p => p.PoDetails.Any(pod => pod.PoDetailsId == firstOldStockG.PoDetailsId))?.NewPurchaseOrder;
                if (newPurchaseOrder != null)
                {
                    if (!newPurchaseOrders.Any(npo => npo.Id == newPurchaseOrder.Id))
                    {
                        throw new Exception("No matching purchase order id");
                    }
                    newStockReceive.PurchaseOrderId = newPurchaseOrder.Id;
                    newStockReceive.StockSouce = StockSource.PurchaseOrder;
                    newStockReceive.SupplierId = newPurchaseOrder.SupplierId;
                }
                else
                {
                    newStockReceive.PurchaseOrderId = null;
                    newStockReceive.StockSouce = StockSource.Direct;
                    newStockReceive.SupplierId = newDeletedSupplier.Id;
                }

                //branch
                var oldBranchNo = oldStockG.FirstOrDefault()?.BranchNo ?? "";
                New.Branch newBranch = newBranches.FirstOrDefault(b => b.BranchNo == BranchNumberComverter(oldBranchNo));
                newStockReceive.BranchId = newBranch?.Id ?? newDeletedBranch.Id;

                foreach (var oldStock in oldStockG)
                {
                    New.Stock newStock = new New.Stock();
                    newStock.CreatedAt = DateTimeOffset.Now;
                    newStock.StockNumber = oldStock.StockNo;
                    newStock.StockStatus = Enum.Parse<StockStatus>(oldStock.Status);
                    newStock.CostPrice = StringToMoneyConverter(oldStock.Price);
                    newStock.SerialNumber = getSerialNumber(oldStock.SerialNo, newStocks);

                    newStock.BranchId = newBranch?.Id ?? newDeletedBranch.Id;

                    newStock.MasterProductId = newMasterProducts.FirstOrDefault(m => m.Model == oldStock.Model)?.Id ?? newDeletedMasterProduct.Id;

                    var newPurchaseOrderItem = oldPurchaseOrderItems.FirstOrDefault(poi => poi.PoDetailsId == oldStock.PoDetailsId)?.NewPurchaseOrderItem;

                    var newPO = newPurchaseOrders.FirstOrDefault(po => po.Id == newPurchaseOrderItem?.PurchaseOrderId);

                    var supplierId = newPO?.SupplierId;

                    newStock.SupplierId = supplierId ?? newDeletedSupplier.Id;

                    newStockReceive.Stocks.Add(newStock);
                    newStocks.Add(newStock);
                }

                Console.WriteLine("Stock receive added");
                newStockReceives.Add(newStockReceive);
            });
            //foreach (var oldStockG in oldStocksGroup)
            //{

            //}
            //process duplicate serial
            Parallel.ForEach(newStocks.GroupBy(s => s.SerialNumber).Where(g => g.Count() > 1), sg =>
            {
                int i = 2;
                foreach (var stock in sg)
                {
                    if (stock != sg.FirstOrDefault())
                    {
                        stock.SerialNumber = $"{stock.SerialNumber}-DUPLICATE-{i++}";
                    }
                }
            });

            Console.WriteLine("Saving Stock receives");
            using (var newDbContext = services.GetRequiredService<CADataContext>())
            {
                newDbContext.StockReceives.AddRange(newStockReceives);
                await newDbContext.SaveChangesAsync();
            }

        }

        private static string GetBranchForPO(string branchAddress)
        {
            Dictionary<string, string> poBranchMapping = new Dictionary<string, string>();
            poBranchMapping.Add("SAN JOSE ANTIQUE", "06");
            poBranchMapping.Add("Aldeguer St. Iloilo City", "02");
            poBranchMapping.Add("Magsaysay St. Roxa Oriental Mindoro", "16");
            poBranchMapping.Add("TAYTAY, PALAWAN", "26");
            poBranchMapping.Add("SJOM", "12");
            poBranchMapping.Add("SABLAYAN, MINDORO", "13");
            poBranchMapping.Add("RXOM", "16");
            poBranchMapping.Add("Rizal St. Kalibo, Aklan", "05");
            poBranchMapping.Add("RIO TUBA, PALAWAN", "07");
            poBranchMapping.Add("PUERTO PRINCESA, PALAWAN", "07");
            poBranchMapping.Add("Preciado St. San Jose Antique", "06");
            poBranchMapping.Add("Plaridel St. Roxas City", "04");
            poBranchMapping.Add("NARRA, PALAWAN", "09");
            poBranchMapping.Add("Manuel Quezon St. Liwanag Odiongan, Romblon", "14");
            poBranchMapping.Add("MAMBURAO, MINDORO", "18");
            poBranchMapping.Add("KALIBO, AKLAN", "05");
            poBranchMapping.Add("Junction 2 National Hi-Way San Pedro, Puerto Princesa, Palawan", "07");
            poBranchMapping.Add("CUYO, PALAWAN", "15");
            poBranchMapping.Add("CORON, PALAWAN", "17");
            poBranchMapping.Add("CATICLAN, AKLAN", "28");
            poBranchMapping.Add("ALDEGUER ILOILO CITY", "02");
            poBranchMapping.Add("#1 Sta. Fe Bldg. Liboro St. San Jose Mindoro", "12");


            if (!poBranchMapping.TryGetValue(branchAddress, out string nBranchNo))
            {
                Console.WriteLine(branchAddress);
            }

            return nBranchNo;
        }

        private static decimal StringToMoneyConverter(string sMoney)
        {
            var moneyStringCleaners = new List<IMoneyStringCleaner>() {
                new RemoveTrailingDot(),
                new ZeroIfAllNotDigitCharacters(),
                new RemoveTrailingZeroAndAllDot()
            };
            bool formatSuccess;
            decimal dMoney;

            formatSuccess = decimal.TryParse(sMoney, out dMoney);

            if (!formatSuccess)
            {
                foreach (var moneyStringCleaner in moneyStringCleaners)
                {
                    sMoney = moneyStringCleaner.CleanMoneyString(sMoney);
                    formatSuccess = decimal.TryParse(sMoney, out dMoney);
                    if (formatSuccess)
                    {
                        break;
                    }
                }
            }
            if (!formatSuccess)
            {
                var nfi = new CultureInfo("en-US", false).NumberFormat;
                nfi.NumberGroupSeparator = ".";

                formatSuccess = decimal.TryParse(sMoney, NumberStyles.Any, nfi, out dMoney);
            }
            if (!formatSuccess)
            {
                throw new Exception("Error parsing money");
            }
            return dMoney;
        }
        private static int serialCounter = 1;
        private static object _lock = new object();
        private static List<string> emptySerials = new List<string>() { "-", string.Empty, "-0-" };
        private static string getSerialNumber(string oldSerialNumber, ConcurrentBag<New.Stock> newStocks)
        {

            if (emptySerials.Contains(oldSerialNumber))
            {
                lock (_lock)
                {
                    return "EMPTY-" + serialCounter++;
                }

            }
            return oldSerialNumber;

        }


        public static int BranchNumberComverter(string oldBranchNumber)
        {
            string strBranchNo = oldBranchNumber;
            int ret = 0;
            if (!strBranchNo.All(char.IsDigit))
            {
                strBranchNo = String.Concat(strBranchNo.Where(char.IsDigit));
                ret = Convert.ToInt32(strBranchNo) * -1;
            }
            else
            {
                ret = Convert.ToInt32(strBranchNo);
            }
            return ret;
        }

        private static CitiAppDatabaseContext GetOldDbContext()
        {
            var connectionstring = Program.GetConfiguration().GetSection("connectionStrings")["OldDbConnection"];

            var optionsBuilder = new DbContextOptionsBuilder<CitiAppDatabaseContext>();
            optionsBuilder.UseSqlServer(connectionstring);
            return new CitiAppDatabaseContext(optionsBuilder.Options);
        }

        //public static CADataContext services.GetRequiredService<CADataContext>()
        //{
        //    var connectionstring = Program.GetConfiguration().GetSection("connectionStrings")["DefaultConnection"];

        //    var optionsBuilder = new DbContextOptionsBuilder<CADataContext>();
        //    optionsBuilder.UseSqlServer(connectionstring);
        //    optionsBuilder.EnableSensitiveDataLogging();
        //    var db = new CADataContext(optionsBuilder.Options);
        //    db.Database.Migrate();
        //    return db;
        //}
    }
}
