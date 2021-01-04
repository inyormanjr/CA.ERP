using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CA.ERP.Utilities.PrevDataModel
{
    public partial class CitiAppDatabaseContext : DbContext
    {
        public CitiAppDatabaseContext()
        {
        }

        public CitiAppDatabaseContext(DbContextOptions<CitiAppDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccDelTable> AccDelTables { get; set; }
        public virtual DbSet<ArrivalTable> ArrivalTables { get; set; }
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<CTransTable> CTransTables { get; set; }
        public virtual DbSet<ChangesLog> ChangesLogs { get; set; }
        public virtual DbSet<Collection> Collections { get; set; }
        public virtual DbSet<CollectionDetail> CollectionDetails { get; set; }
        public virtual DbSet<CompanyProfile> CompanyProfiles { get; set; }
        public virtual DbSet<CustomerTable> CustomerTables { get; set; }
        public virtual DbSet<DeliveryReceipt> DeliveryReceipts { get; set; }
        public virtual DbSet<DrDetail> DrDetails { get; set; }
        public virtual DbSet<FreeProduct> FreeProducts { get; set; }
        public virtual DbSet<MigrationHistory> MigrationHistories { get; set; }
        public virtual DbSet<Model> Models { get; set; }
        public virtual DbSet<OfTable> OfTables { get; set; }
        public virtual DbSet<PoDetail> PoDetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductList> ProductLists { get; set; }
        public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual DbSet<StRequisitionTable> StRequisitionTables { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<TransferTable> TransferTables { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=database;Database=citiAppDatabase;user id=sa; password=DbPassword123@;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AccDelTable>(entity =>
            {
                entity.ToTable("AccDelTable");

                entity.Property(e => e.AccountNo).HasMaxLength(50);

                entity.Property(e => e.DelBy).HasMaxLength(50);

                entity.Property(e => e.DelDateTime).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasMaxLength(50);
            });

            modelBuilder.Entity<ArrivalTable>(entity =>
            {
                entity.HasKey(e => e.DeliveryNo);

                entity.ToTable("arrivalTable");

                entity.Property(e => e.DeliveryNo)
                    .HasMaxLength(50)
                    .HasColumnName("deliveryNo");

                entity.Property(e => e.BranchNo)
                    .HasMaxLength(50)
                    .HasColumnName("branchNo");

                entity.Property(e => e.DatePrinted)
                    .HasColumnType("date")
                    .HasColumnName("datePrinted");

                entity.Property(e => e.DateReceived)
                    .HasColumnType("date")
                    .HasColumnName("dateReceived");

                entity.Property(e => e.SName).HasColumnName("sName");

                entity.Property(e => e.Via).HasColumnName("VIA");

                entity.Property(e => e.Waybill).HasColumnName("WAYBILL");
            });

            modelBuilder.Entity<Branch>(entity =>
            {
                entity.HasKey(e => e.BranchNo);

                entity.ToTable("branch");

                entity.Property(e => e.BranchNo)
                    .HasMaxLength(50)
                    .HasColumnName("branchNo");

                entity.Property(e => e.BAddress)
                    .HasMaxLength(50)
                    .HasColumnName("bAddress");

                entity.Property(e => e.BContactNo)
                    .HasMaxLength(50)
                    .HasColumnName("bContactNo");

                entity.Property(e => e.BranchCode)
                    .HasMaxLength(50)
                    .HasColumnName("branchCode");

                entity.Property(e => e.BranchName)
                    .HasMaxLength(50)
                    .HasColumnName("branchName");

                entity.Property(e => e.IndexNo)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("indexNo");
            });

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.ToTable("brand");

                entity.Property(e => e.BrandId)
                    .HasMaxLength(50)
                    .HasColumnName("brandID");

                entity.Property(e => e.BrandName)
                    .HasMaxLength(50)
                    .HasColumnName("brandName");

                entity.Property(e => e.SupIdno)
                    .HasMaxLength(50)
                    .HasColumnName("sup_IDno");
            });

            modelBuilder.Entity<CTransTable>(entity =>
            {
                entity.HasKey(e => e.TransId);

                entity.ToTable("c_TransTable");

                entity.HasIndex(e => e.CTransStatus, "_dta_index_c_TransTable_7_1710629137__K15_1_2_3_4_5_6_7_8_9_10_11_12_13_14_16_17_18_19_20_21_22");

                entity.HasIndex(e => new { e.BranchNo, e.OrNum }, "_dta_index_c_TransTable_7_1710629137__K18_K3_1_2_4_5_6_7_8_9_10_11_12_13_14_15_16_17_19_20_21_22");

                entity.HasIndex(e => e.OrNum, "_dta_index_c_TransTable_7_1710629137__K3");

                entity.HasIndex(e => e.AccountNo, "_dta_index_c_TransTable_7_1710629137__K5");

                entity.Property(e => e.TransId).HasColumnName("trans_Id");

                entity.Property(e => e.AccountNo).HasMaxLength(50);

                entity.Property(e => e.Bank)
                    .HasMaxLength(50)
                    .HasColumnName("bank");

                entity.Property(e => e.BranchNo)
                    .HasMaxLength(50)
                    .HasColumnName("branchNo");

                entity.Property(e => e.CTransStatus)
                    .HasMaxLength(50)
                    .HasColumnName("cTransStatus");

                entity.Property(e => e.CashAmt)
                    .HasMaxLength(50)
                    .HasColumnName("cash_AMT");

                entity.Property(e => e.Change)
                    .HasMaxLength(50)
                    .HasColumnName("change");

                entity.Property(e => e.ChequeAmt)
                    .HasMaxLength(50)
                    .HasColumnName("cheque_AMT");

                entity.Property(e => e.ChequeNo)
                    .HasMaxLength(50)
                    .HasColumnName("chequeNo");

                entity.Property(e => e.Disc)
                    .HasMaxLength(50)
                    .HasColumnName("DISC");

                entity.Property(e => e.GrsAmt)
                    .HasMaxLength(50)
                    .HasColumnName("GRS_AMT");

                entity.Property(e => e.IdNumber)
                    .HasMaxLength(50)
                    .HasColumnName("ID_Number");

                entity.Property(e => e.Int)
                    .HasMaxLength(50)
                    .HasColumnName("INT");

                entity.Property(e => e.NetAmt)
                    .HasMaxLength(50)
                    .HasColumnName("NET_AMT");

                entity.Property(e => e.OrNum)
                    .HasMaxLength(50)
                    .HasColumnName("OR_NUM");

                entity.Property(e => e.PayType)
                    .HasMaxLength(50)
                    .HasColumnName("pay_Type");

                entity.Property(e => e.Payment)
                    .HasMaxLength(50)
                    .HasColumnName("PAYMENT");

                entity.Property(e => e.Rebate)
                    .HasMaxLength(50)
                    .HasColumnName("REBATE");

                entity.Property(e => e.Remarks)
                    .HasMaxLength(50)
                    .HasColumnName("remarks");

                entity.Property(e => e.SystemDate)
                    .HasColumnType("date")
                    .HasColumnName("system_Date");

                entity.Property(e => e.TransDate)
                    .HasColumnType("date")
                    .HasColumnName("trans_Date");

                entity.Property(e => e.TransType)
                    .HasMaxLength(50)
                    .HasColumnName("trans_Type");
            });

            modelBuilder.Entity<ChangesLog>(entity =>
            {
                entity.ToTable("ChangesLog");

                entity.Property(e => e.ChangeType)
                    .HasMaxLength(50)
                    .HasColumnName("changeType");

                entity.Property(e => e.DateChange).HasColumnName("dateChange");

                entity.Property(e => e.Details).HasMaxLength(50);
            });

            modelBuilder.Entity<Collection>(entity =>
            {
                entity.HasKey(e => e.CollectionId);

                entity.ToTable("collection");

                entity.Property(e => e.CollectionId)
                    .HasMaxLength(50)
                    .HasColumnName("Collection_ID");

                entity.Property(e => e.Balance).HasMaxLength(50);

                entity.Property(e => e.DeliveryFee)
                    .HasMaxLength(50)
                    .HasColumnName("Delivery_Fee");

                entity.Property(e => e.GrsMonthly)
                    .HasMaxLength(50)
                    .HasColumnName("GRS_Monthly");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.MonthlyRebate)
                    .HasMaxLength(50)
                    .HasColumnName("monthlyRebate");

                entity.Property(e => e.NetMonthly)
                    .HasMaxLength(50)
                    .HasColumnName("Net_monthly");

                entity.Property(e => e.NotarialFee)
                    .HasMaxLength(50)
                    .HasColumnName("Notarial_Fee");

                entity.Property(e => e.OtherPay)
                    .HasMaxLength(50)
                    .HasColumnName("Other_Pay");

                entity.Property(e => e.Payment).HasMaxLength(50);

                entity.Property(e => e.PaymentType)
                    .HasMaxLength(50)
                    .HasColumnName("paymentType");

                entity.Property(e => e.Pn)
                    .HasMaxLength(50)
                    .HasColumnName("PN");

                entity.Property(e => e.Remarks)
                    .HasMaxLength(50)
                    .HasColumnName("remarks");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.Terms)
                    .HasMaxLength(50)
                    .HasColumnName("terms");

                entity.Property(e => e.TotalLcp)
                    .HasMaxLength(50)
                    .HasColumnName("Total_LCP");

                entity.Property(e => e.TotalRebate)
                    .HasMaxLength(50)
                    .HasColumnName("Total_Rebate");

                entity.Property(e => e.TransactionDate)
                    .HasColumnType("date")
                    .HasColumnName("Transaction_Date");

                entity.Property(e => e.Udi)
                    .HasMaxLength(50)
                    .HasColumnName("UDI");
            });

            modelBuilder.Entity<CollectionDetail>(entity =>
            {
                entity.ToTable("collection_details");

                entity.HasIndex(e => e.CollectionId, "_dta_index_collection_details_7_1070626857__K13_1_2_3_4_5_6_7_8_9_10_11_12_14_15_16_17_18_19_20");

                entity.HasIndex(e => new { e.CollectionId, e.No, e.OrNumber }, "_dta_index_collection_details_7_1070626857__K13_K4_K5_1_2_3_6_7_8_9_10_11_12_14_15_16_17_18_19_20");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AcctBalance)
                    .HasMaxLength(50)
                    .HasColumnName("Acct_Balance");

                entity.Property(e => e.ColChequeAmt)
                    .HasMaxLength(50)
                    .HasColumnName("col_cheque_amt");

                entity.Property(e => e.CollCashAmt)
                    .HasMaxLength(50)
                    .HasColumnName("coll_cash_amt");

                entity.Property(e => e.CollectionDetailsId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Collection_Details_ID");

                entity.Property(e => e.CollectionId)
                    .HasMaxLength(50)
                    .HasColumnName("Collection_ID");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Discount).HasMaxLength(50);

                entity.Property(e => e.ExpectedBalance)
                    .HasMaxLength(50)
                    .HasColumnName("expected_balance");

                entity.Property(e => e.ExpectedDate)
                    .HasColumnType("date")
                    .HasColumnName("expected_date");

                entity.Property(e => e.No)
                    .HasMaxLength(50)
                    .HasColumnName("NO");

                entity.Property(e => e.OrNumber)
                    .HasMaxLength(50)
                    .HasColumnName("OR_Number");

                entity.Property(e => e.PaymentType)
                    .HasMaxLength(50)
                    .HasColumnName("paymentType");

                entity.Property(e => e.Penalty).HasMaxLength(50);

                entity.Property(e => e.PrinAmount)
                    .HasMaxLength(50)
                    .HasColumnName("PRIN_Amount");

                entity.Property(e => e.Rebate).HasMaxLength(50);

                entity.Property(e => e.Remarks)
                    .HasMaxLength(50)
                    .HasColumnName("remarks");

                entity.Property(e => e.StockNo)
                    .HasMaxLength(50)
                    .HasColumnName("stockNo");

                entity.Property(e => e.TotalAmount)
                    .HasMaxLength(50)
                    .HasColumnName("Total_Amount");

                entity.Property(e => e.TransactionDate)
                    .HasColumnType("date")
                    .HasColumnName("transaction_date");
            });

            modelBuilder.Entity<CompanyProfile>(entity =>
            {
                entity.HasKey(e => e.CitiProfileNo);

                entity.ToTable("companyProfile");

                entity.Property(e => e.CitiProfileNo)
                    .HasMaxLength(50)
                    .HasColumnName("citiProfileNo");

                entity.Property(e => e.BranchId)
                    .HasMaxLength(50)
                    .HasColumnName("branchID");

                entity.Property(e => e.CedulaDateissued)
                    .HasColumnType("date")
                    .HasColumnName("cedula_dateissued");

                entity.Property(e => e.CedulaName).HasColumnName("cedula_name");

                entity.Property(e => e.CedulaNo)
                    .HasMaxLength(50)
                    .HasColumnName("cedula_no");

                entity.Property(e => e.IndexNo)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("indexNo");

                entity.Property(e => e.PlaceIssued)
                    .HasMaxLength(50)
                    .HasColumnName("place_issued");
            });

            modelBuilder.Entity<CustomerTable>(entity =>
            {
                entity.HasKey(e => e.IdNumber);

                entity.ToTable("customerTable");

                entity.HasIndex(e => e.FullName, "_dta_index_customerTable_7_2105058535__K12_1_2_3_4_5_6_7_8_9_10_11");

                entity.HasIndex(e => new { e.IdNumber, e.LastName, e.MidName, e.FName, e.FullName }, "_dta_index_customerTable_7_2105058535__K1_K2_K3_K4_K12");

                entity.Property(e => e.IdNumber)
                    .HasMaxLength(50)
                    .HasColumnName("ID_Number");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .HasColumnName("address");

                entity.Property(e => e.CivilStatus)
                    .HasMaxLength(50)
                    .HasColumnName("civil_status");

                entity.Property(e => e.CoMaker)
                    .HasMaxLength(50)
                    .HasColumnName("co_Maker");

                entity.Property(e => e.CoMakerAddress)
                    .HasMaxLength(50)
                    .HasColumnName("co_MakerAddress");

                entity.Property(e => e.Contact)
                    .HasMaxLength(50)
                    .HasColumnName("contact");

                entity.Property(e => e.DateRegistered).HasColumnType("datetime");

                entity.Property(e => e.Employer)
                    .HasMaxLength(50)
                    .HasColumnName("employer");

                entity.Property(e => e.EmployerAddress)
                    .HasMaxLength(50)
                    .HasColumnName("employerAddress");

                entity.Property(e => e.FName)
                    .HasMaxLength(50)
                    .HasColumnName("f_Name");

                entity.Property(e => e.FullName)
                    .HasMaxLength(50)
                    .HasColumnName("fullName");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .HasColumnName("last_Name");

                entity.Property(e => e.MidName)
                    .HasMaxLength(50)
                    .HasColumnName("mid_Name");

                entity.Property(e => e.RegisteredBy).HasMaxLength(50);
            });

            modelBuilder.Entity<DeliveryReceipt>(entity =>
            {
                entity.HasKey(e => e.AccountNo);

                entity.ToTable("deliveryReceipt");

                entity.HasIndex(e => e.Id, "_dta_index_deliveryReceipt_7_1102626971__K1D_2_3_4_5_6_7_8_9_10_11_12_13");

                entity.HasIndex(e => e.IdNumber, "_dta_index_deliveryReceipt_7_1102626971__K8_1_2_3_4_5_6_7_9_10_11_12_13");

                entity.Property(e => e.AccountNo).HasMaxLength(50);

                entity.Property(e => e.AccountType)
                    .HasMaxLength(50)
                    .HasColumnName("Account_Type");

                entity.Property(e => e.BranchNo)
                    .HasMaxLength(50)
                    .HasColumnName("branchNo");

                entity.Property(e => e.CiBy)
                    .HasMaxLength(50)
                    .HasColumnName("CI_By");

                entity.Property(e => e.CollectionId)
                    .HasMaxLength(50)
                    .HasColumnName("Collection_ID");

                entity.Property(e => e.DeliveryDate)
                    .HasColumnType("date")
                    .HasColumnName("Delivery_Date");

                entity.Property(e => e.DrNo)
                    .HasMaxLength(50)
                    .HasColumnName("DR_no");

                entity.Property(e => e.Drtype)
                    .HasMaxLength(50)
                    .HasColumnName("DRtype");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.IdNumber)
                    .HasMaxLength(50)
                    .HasColumnName("ID_Number");

                entity.Property(e => e.Salesman).HasMaxLength(50);

                entity.Property(e => e.SiDrNo)
                    .HasMaxLength(50)
                    .HasColumnName("SI_DR_no");

                entity.Property(e => e.Status).HasMaxLength(50);
            });

            modelBuilder.Entity<DrDetail>(entity =>
            {
                entity.ToTable("DR_details");

                entity.HasIndex(e => e.AccountNo, "_dta_index_DR_details_7_1134627085__K5_1_2_3_4_6_7_8_9_10_11_12_13_14_15_16_17_18_19_20");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccountNo).HasMaxLength(50);

                entity.Property(e => e.BalanceAf)
                    .HasMaxLength(50)
                    .HasColumnName("BalanceAF");

                entity.Property(e => e.Brand)
                    .HasMaxLength(50)
                    .HasColumnName("brand");

                entity.Property(e => e.Cash)
                    .HasMaxLength(50)
                    .HasColumnName("cash");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .HasColumnName("description");

                entity.Property(e => e.DownPayment)
                    .HasMaxLength(50)
                    .HasColumnName("down_payment");

                entity.Property(e => e.DrDetailsId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("DR_Details_ID");

                entity.Property(e => e.Lcp)
                    .HasMaxLength(50)
                    .HasColumnName("LCP");

                entity.Property(e => e.Model)
                    .HasMaxLength(50)
                    .HasColumnName("model");

                entity.Property(e => e.OrAmt)
                    .HasMaxLength(50)
                    .HasColumnName("orAmt");

                entity.Property(e => e.OrNumber)
                    .HasMaxLength(50)
                    .HasColumnName("or_number");

                entity.Property(e => e.PStatus)
                    .HasMaxLength(50)
                    .HasColumnName("pStatus");

                entity.Property(e => e.PaymentType)
                    .HasMaxLength(50)
                    .HasColumnName("paymentType");

                entity.Property(e => e.Pn)
                    .HasMaxLength(50)
                    .HasColumnName("PN");

                entity.Property(e => e.Qnty)
                    .HasMaxLength(50)
                    .HasColumnName("qnty");

                entity.Property(e => e.Remarks)
                    .HasMaxLength(100)
                    .HasColumnName("remarks");

                entity.Property(e => e.SerialNo)
                    .HasMaxLength(50)
                    .HasColumnName("serialNo");

                entity.Property(e => e.StockNo)
                    .HasMaxLength(50)
                    .HasColumnName("stockNo");

                entity.Property(e => e.TermsDr)
                    .HasMaxLength(50)
                    .HasColumnName("termsDR");
            });

            modelBuilder.Entity<FreeProduct>(entity =>
            {
                entity.HasKey(e => e.FstockNo);

                entity.ToTable("freeProduct");

                entity.Property(e => e.FstockNo).HasMaxLength(50);

                entity.Property(e => e.AttachedStockNo)
                    .HasMaxLength(50)
                    .HasColumnName("attachedStockNo");

                entity.Property(e => e.IndexNo)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("indexNo");

                entity.Property(e => e.StockNo)
                    .HasMaxLength(50)
                    .HasColumnName("stockNo");
            });

            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey })
                    .HasName("PK_dbo.__MigrationHistory");

                entity.ToTable("__MigrationHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ContextKey).HasMaxLength(300);

                entity.Property(e => e.Model).IsRequired();

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<Model>(entity =>
            {
                entity.ToTable("model");

                entity.Property(e => e.ModelId)
                    .HasMaxLength(50)
                    .HasColumnName("modelID");

                entity.Property(e => e.BrandId)
                    .HasMaxLength(50)
                    .HasColumnName("brandID");

                entity.Property(e => e.ModelName)
                    .HasMaxLength(50)
                    .HasColumnName("modelName");
            });

            modelBuilder.Entity<OfTable>(entity =>
            {
                entity.HasKey(e => e.Ofid);

                entity.ToTable("OF_Table");

                entity.Property(e => e.Ofid)
                    .HasMaxLength(50)
                    .HasColumnName("OFID");

                entity.Property(e => e.CashAmt)
                    .HasMaxLength(50)
                    .HasColumnName("cash_amt");

                entity.Property(e => e.ChequeAmt)
                    .HasMaxLength(50)
                    .HasColumnName("cheque_amt");

                entity.Property(e => e.FeeType).HasMaxLength(50);

                entity.Property(e => e.Ornumber)
                    .HasMaxLength(50)
                    .HasColumnName("ORNumber");

                entity.Property(e => e.PaymentType)
                    .HasMaxLength(50)
                    .HasColumnName("paymentType");

                entity.Property(e => e.Payments)
                    .HasMaxLength(50)
                    .HasColumnName("payments");

                entity.Property(e => e.Remarks).HasMaxLength(50);

                entity.Property(e => e.TransId).HasColumnName("trans_Id");
            });

            modelBuilder.Entity<PoDetail>(entity =>
            {
                entity.HasKey(e => e.PoDetailsId);

                entity.ToTable("poDetails");

                entity.Property(e => e.PoDetailsId)
                    .HasMaxLength(50)
                    .HasColumnName("poDetails_ID");

                entity.Property(e => e.Brand)
                    .HasMaxLength(50)
                    .HasColumnName("brand");

                entity.Property(e => e.Cost)
                    .HasMaxLength(50)
                    .HasColumnName("cost");

                entity.Property(e => e.Discount)
                    .HasMaxLength(50)
                    .HasColumnName("discount");

                entity.Property(e => e.FreeQty)
                    .HasMaxLength(50)
                    .HasColumnName("freeQTY");

                entity.Property(e => e.HolderQty)
                    .HasMaxLength(50)
                    .HasColumnName("holderQTY");

                entity.Property(e => e.IndexNo)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("indexNo");

                entity.Property(e => e.Model)
                    .HasMaxLength(50)
                    .HasColumnName("model");

                entity.Property(e => e.OrderedQty)
                    .HasMaxLength(50)
                    .HasColumnName("orderedQTY");

                entity.Property(e => e.PoId)
                    .HasMaxLength(50)
                    .HasColumnName("poID");

                entity.Property(e => e.RemainingQty)
                    .HasMaxLength(50)
                    .HasColumnName("remainingQTY");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .HasColumnName("status");

                entity.Property(e => e.TotalCost)
                    .HasMaxLength(50)
                    .HasColumnName("totalCost");

                entity.Property(e => e.TotalQty)
                    .HasMaxLength(50)
                    .HasColumnName("totalQty");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.StockNo);

                entity.ToTable("products");

                entity.HasIndex(e => new { e.LocFrom, e.LocTo }, "_dta_index_products_7_1230627427__K9_K10_1_2_3_4_5_6_7_8_11_12_13_14");

                entity.Property(e => e.StockNo)
                    .HasMaxLength(50)
                    .HasColumnName("stockNo");

                entity.Property(e => e.BranchNo)
                    .HasMaxLength(50)
                    .HasColumnName("branchNo");

                entity.Property(e => e.Brand)
                    .HasMaxLength(50)
                    .HasColumnName("brand");

                entity.Property(e => e.DateReceived)
                    .HasColumnType("date")
                    .HasColumnName("date_Received");

                entity.Property(e => e.DeliveryNo)
                    .HasMaxLength(50)
                    .HasColumnName("deliveryNo");

                entity.Property(e => e.LocFrom)
                    .HasMaxLength(50)
                    .HasColumnName("loc_From");

                entity.Property(e => e.LocTo)
                    .HasMaxLength(50)
                    .HasColumnName("loc_To");

                entity.Property(e => e.Model)
                    .HasMaxLength(50)
                    .HasColumnName("model");

                entity.Property(e => e.PoDetailsId)
                    .HasMaxLength(50)
                    .HasColumnName("poDetails_ID");

                entity.Property(e => e.Price)
                    .HasMaxLength(50)
                    .HasColumnName("price");

                entity.Property(e => e.ProdStatus)
                    .HasMaxLength(50)
                    .HasColumnName("prodStatus");

                entity.Property(e => e.SerialNo)
                    .HasMaxLength(50)
                    .HasColumnName("serialNo");

                entity.Property(e => e.StId)
                    .HasMaxLength(50)
                    .HasColumnName("st_ID");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .HasColumnName("status");
            });

            modelBuilder.Entity<ProductList>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.ToTable("productList");

                entity.Property(e => e.ProductId)
                    .HasMaxLength(50)
                    .HasColumnName("product_ID");

                entity.Property(e => e.BrandId)
                    .HasMaxLength(50)
                    .HasColumnName("brandID");

                entity.Property(e => e.ModelId)
                    .HasMaxLength(50)
                    .HasColumnName("modelID");

                entity.Property(e => e.Price)
                    .HasMaxLength(50)
                    .HasColumnName("price");

                entity.Property(e => e.SupIdno)
                    .HasMaxLength(50)
                    .HasColumnName("sup_IDno");
            });

            modelBuilder.Entity<PurchaseOrder>(entity =>
            {
                entity.HasKey(e => e.PoId);

                entity.ToTable("purchaseOrder");

                entity.Property(e => e.PoId)
                    .HasMaxLength(50)
                    .HasColumnName("poID");

                entity.Property(e => e.ApprovedBy)
                    .HasMaxLength(50)
                    .HasColumnName("approvedBy");

                entity.Property(e => e.BranchNo).HasColumnName("branchNo");

                entity.Property(e => e.DeliveryDate)
                    .HasColumnType("date")
                    .HasColumnName("deliveryDate");

                entity.Property(e => e.IndexNo)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("indexNo");

                entity.Property(e => e.PoDate)
                    .HasColumnType("date")
                    .HasColumnName("poDate");

                entity.Property(e => e.SupIdno)
                    .HasMaxLength(50)
                    .HasColumnName("sup_IDno");

                entity.Property(e => e.TotalAmount)
                    .HasMaxLength(50)
                    .HasColumnName("totalAmount");

                entity.HasMany(e => e.PoDetails)
                .WithOne()
                .HasForeignKey(e => e.PoId);
            });

            modelBuilder.Entity<StRequisitionTable>(entity =>
            {
                entity.HasKey(e => e.StId);

                entity.ToTable("st_requisitionTable");

                entity.Property(e => e.StId)
                    .HasMaxLength(50)
                    .HasColumnName("st_ID");

                entity.Property(e => e.DateTransaction)
                    .HasColumnType("datetime")
                    .HasColumnName("date_Transaction");

                entity.Property(e => e.FromLocation)
                    .HasMaxLength(50)
                    .HasColumnName("from_location");

                entity.Property(e => e.GatePassNo)
                    .HasMaxLength(50)
                    .HasColumnName("gatePassNo");

                entity.Property(e => e.ReceivedBy).HasColumnName("receivedBy");

                entity.Property(e => e.ReleasedBy).HasColumnName("releasedBy");

                entity.Property(e => e.TransferLocation)
                    .HasMaxLength(50)
                    .HasColumnName("transfer_location");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.HasKey(e => e.SupIdno);

                entity.ToTable("supplier");

                entity.Property(e => e.SupIdno)
                    .HasMaxLength(50)
                    .HasColumnName("sup_IDno");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .HasColumnName("address");

                entity.Property(e => e.Contact)
                    .HasMaxLength(50)
                    .HasColumnName("contact");

                entity.Property(e => e.IndexNo)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("indexNo");

                entity.Property(e => e.SName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("sName");
            });

            modelBuilder.Entity<TransferTable>(entity =>
            {
                entity.HasKey(e => e.TransferId);

                entity.ToTable("transferTable");

                entity.Property(e => e.TransferId)
                    .HasMaxLength(50)
                    .HasColumnName("transfer_ID");

                entity.Property(e => e.Qnty)
                    .HasMaxLength(50)
                    .HasColumnName("qnty");

                entity.Property(e => e.StId)
                    .HasMaxLength(50)
                    .HasColumnName("st_ID");

                entity.Property(e => e.StockNo)
                    .HasMaxLength(50)
                    .HasColumnName("stockNo");

                entity.Property(e => e.TransDate)
                    .HasColumnType("date")
                    .HasColumnName("trans_Date");

                entity.Property(e => e.TransferLocation)
                    .HasMaxLength(50)
                    .HasColumnName("transferLocation");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .HasColumnName("userID");

                entity.Property(e => e.BranchNo)
                    .HasMaxLength(50)
                    .HasColumnName("branchNo");

                entity.Property(e => e.FName)
                    .HasMaxLength(50)
                    .HasColumnName("F_name");

                entity.Property(e => e.IndexNo)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("indexNo");

                entity.Property(e => e.LName)
                    .HasMaxLength(50)
                    .HasColumnName("L_name");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasColumnName("password");

                entity.Property(e => e.Role)
                    .HasMaxLength(50)
                    .HasColumnName("role");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
