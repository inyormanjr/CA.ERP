using CA.ERP.DataAccess;
using CA.ERP.DataAccess.Entities;
using CA.ERP.Domain.Helpers;
using CA.ERP.Utilities.PrevDataModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using New = CA.ERP.DataAccess.Entities;

namespace CA.ERP.Utilities
{
    public class Migrator
    {
        public static async Task MigrateScoped()
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await Migrate().ConfigureAwait(true);
                scope.Complete();
            }
        }
        public static async Task Migrate()
        {
            CitiAppDatabaseContext citiAppDatabaseContext = GetOldDbContext();

            //migrate supplier
            var oldSuppliers = await citiAppDatabaseContext.Suppliers.ToListAsync();

            var newSuppliers = new List<New.Supplier>();
            foreach (var supplier in oldSuppliers)
            {
                New.Supplier newSupplier = new New.Supplier();
                newSupplier.Name = supplier.SName;
                newSupplier.Address = supplier.Address;
                newSupplier.ContactPerson = supplier.Contact;

                newSuppliers.Add(newSupplier);
            }
            //save suppliers
            using (var newDbContext = GetCADataContext())
            {
                newDbContext.Suppliers.AddRange(newSuppliers);
                await newDbContext.SaveChangesAsync();
            }

            //migrate brands

            var oldBrands = await citiAppDatabaseContext.Brands.ToListAsync();
            var newBrands = new List<New.Brand>();
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

            using (var newDbContext = GetCADataContext())
            {
                newDbContext.Brands.AddRange(newBrands);
                await newDbContext.SaveChangesAsync();
            }

            //migrate model to masterproducts 
            var oldModels = await citiAppDatabaseContext.Models.ToListAsync();
            //var productLists = await citiAppDatabaseContext.ProductLists.ToListAsync();

            var newMasterProducts = new List<New.MasterProduct>();

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

            using (var newDbContext = GetCADataContext())
            {
                newDbContext.MasterProducts.AddRange(newMasterProducts);
                await newDbContext.SaveChangesAsync();
            }

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

            using (var newDbContext = GetCADataContext())
            {
                newDbContext.SupplierMasterProducts.AddRange(newSupplierMasterProducts);
                await newDbContext.SaveChangesAsync();
            }


            var oldBranches = await citiAppDatabaseContext.Branches.ToListAsync();
            var newBranches = new List<New.Branch>();
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

            using (var newDbContext = GetCADataContext())
            {
                newDbContext.Branches.AddRange(newBranches);
                await newDbContext.SaveChangesAsync();
            }

            //generate users
            var oldUsers = citiAppDatabaseContext.Users.ToList();
            var newUsers = new List<New.User>();
            foreach (var oldUser in oldUsers)
            {
                New.User newUser = new New.User();
                newUser.Username = oldUser.Username;
                PasswordGenerator(newUser, oldUser.Password);

                newUser.Role = (UserRole)Enum.Parse(typeof(UserRole), oldUser.Role.Replace("&", ""), true);
                newUser.FirstName = oldUser.FName;
                newUser.LastName = oldUser.LName;

                var newBranch = newBranches.FirstOrDefault(b => b.BranchNo == BranchNumberComverter(oldUser.BranchNo));
                if (newBranch != null)
                {
                    newUser.UserBranches.Add(new UserBranch()
                    {
                        BranchId = newBranch.Id
                    });
                }

                newUsers.Add(newUser);


            }

            using (var newDbContext = GetCADataContext())
            {
                newDbContext.Users.AddRange(newUsers);
                await newDbContext.SaveChangesAsync();
            }
        }

        private static void PasswordGenerator(New.User newUser, string password)
        {
            PasswordManagementHelper passwordManagementHelper = new PasswordManagementHelper();
            byte[] passwordHash;
            byte[] passwordSalt;
            passwordManagementHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            newUser.PasswordHash = passwordHash;
            newUser.PasswordSalt = passwordSalt;

        }

        private static int BranchNumberComverter(string oldBranchNumber)
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

        public static CADataContext GetCADataContext()
        {
            var connectionstring = Program.GetConfiguration().GetSection("connectionStrings")["DefaultConnection"];

            var optionsBuilder = new DbContextOptionsBuilder<CADataContext>();
            optionsBuilder.UseSqlServer(connectionstring);
            optionsBuilder.EnableSensitiveDataLogging();
            var db = new CADataContext(optionsBuilder.Options);
            db.Database.Migrate();
            return db;
        }
    }
}
