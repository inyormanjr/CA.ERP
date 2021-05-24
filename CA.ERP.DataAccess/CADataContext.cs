
using CA.ERP.DataAccess.EFMapping;
using CA.ERP.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.DataAccess
{
    public class CADataContext : DbContext
    {
        public CADataContext(DbContextOptions<CADataContext> options) : base(options)
        {
        }

        public DbSet<Branch> Branches { get; set; }
        //public  DbSet<User> Users { get; set; }
        //public  DbSet<UserBranch> UserBranches { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SupplierBrand> SupplierBrands { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<MasterProduct> MasterProducts { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderItem> PurchaseOrderItems { get; set; }
        public DbSet<SupplierMasterProduct> SupplierMasterProducts { get; set; }
        public DbSet<StockReceive> StockReceives { get; set; }
        public DbSet<StockReceiveItem> StockReceiveItems { get; set; }

        public DbSet<StockTransfer> StockTransfers { get; set; }

        public DbSet<StockTransferItem> StockTransferItems { get; set; }

        public DbSet<Stock> Stocks { get; set; }
        // public DbSet<StockInventory> StockInventories { get; set; }
        // public DbSet<StockMove> StockMoves { get; set; }
        public DbSet<StockCounter> StockCounters { get; set; }
        public DbSet<Bank> Banks { get; set; }
        //public DbSet<Transaction> Transactions { get; set; }
        //public DbSet<Payment> Payments { get; set; }
        //public DbSet<CashPaymentDetail> CashPaymentDetails { get; set; }
        //public DbSet<CardPaymentDetail> CardPaymentDetails { get; set; }
        //public DbSet<ChequePaymentDetail> ChequePaymentDetails { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new BranchMapping());
            builder.ApplyConfiguration(new SupplierMapping());
            builder.ApplyConfiguration(new SupplierBrandMapping());
            builder.ApplyConfiguration(new BrandMapping());
            builder.ApplyConfiguration(new MasterProductMapping());
            builder.ApplyConfiguration(new PurchaseOrderMapping());
            builder.ApplyConfiguration(new PurchaseOrderItemMapping());
            builder.ApplyConfiguration(new SupplierMasterProductMapping());
            builder.ApplyConfiguration(new StockReceiveMapping());
            builder.ApplyConfiguration(new StockReceiveItemMapping());
            builder.ApplyConfiguration(new StockTransferMapping());
            builder.ApplyConfiguration(new StockTransferItemMapping());
            builder.ApplyConfiguration(new StockMapping());
            //builder.ApplyConfiguration(new StockInventoryMapping());
            //builder.ApplyConfiguration(new StockMoveMapping());
            builder.ApplyConfiguration(new StockCounterMapping());
            builder.ApplyConfiguration(new BankMapping());
            //builder.ApplyConfiguration(new TransactionMapping());
            //builder.ApplyConfiguration(new PaymentMapping());
            //builder.ApplyConfiguration(new CashPaymentDetailMapping());
            //builder.ApplyConfiguration(new CardPaymentDetailMapping());
            //builder.ApplyConfiguration(new ChequePaymentDetailMapping());
            builder.ApplyConfiguration(new CustomerMapping());
            //builder.ApplyConfiguration(new UserMapping());
            //builder.ApplyConfiguration(new UserBranchMapping());
            base.OnModelCreating(builder);
        }





    }
}
