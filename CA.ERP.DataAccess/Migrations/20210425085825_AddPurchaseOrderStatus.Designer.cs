﻿// <auto-generated />
using System;
using CA.ERP.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CA.ERP.DataAccess.Migrations
{
    [DbContext(typeof(CADataContext))]
    [Migration("20210425085825_AddPurchaseOrderStatus")]
    partial class AddPurchaseOrderStatus
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.4");

            modelBuilder.Entity("CA.ERP.DataAccess.Entities.Bank", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Banks");
                });

            modelBuilder.Entity("CA.ERP.DataAccess.Entities.Branch", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<int>("BranchNo")
                        .HasColumnType("integer");

                    b.Property<string>("Code")
                        .HasColumnType("text");

                    b.Property<string>("Contact")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Branches");
                });

            modelBuilder.Entity("CA.ERP.DataAccess.Entities.Brand", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("CA.ERP.DataAccess.Entities.MasterProduct", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BrandId")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Model")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("ProductStatus")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("Model")
                        .IsUnique();

                    b.ToTable("MasterProducts");
                });

            modelBuilder.Entity("CA.ERP.DataAccess.Entities.PurchaseOrder", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ApprovedById")
                        .HasColumnType("uuid");

                    b.Property<string>("Barcode")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<DateTimeOffset>("DeliveryDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("DestinationBranchId")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("OrderedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("PurchaseOrderStatus")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<Guid>("SupplierId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("TotalCostPrice")
                        .HasPrecision(10, 2)
                        .HasColumnType("numeric(10,2)");

                    b.HasKey("Id");

                    b.HasIndex("SupplierId");

                    b.HasIndex("DestinationBranchId", "Barcode")
                        .IsUnique();

                    b.ToTable("PurchaseOrders");
                });

            modelBuilder.Entity("CA.ERP.DataAccess.Entities.PurchaseOrderItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("CostPrice")
                        .HasPrecision(10, 2)
                        .HasColumnType("numeric(10,2)");

                    b.Property<decimal>("DeliveredQuantity")
                        .HasPrecision(10, 3)
                        .HasColumnType("numeric(10,3)");

                    b.Property<decimal>("Discount")
                        .HasPrecision(10, 2)
                        .HasColumnType("numeric(10,2)");

                    b.Property<decimal>("FreeQuantity")
                        .HasPrecision(10, 3)
                        .HasColumnType("numeric(10,3)");

                    b.Property<Guid>("MasterProductId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("OrderedQuantity")
                        .HasPrecision(10, 3)
                        .HasColumnType("numeric(10,3)");

                    b.Property<Guid>("PurchaseOrderId")
                        .HasColumnType("uuid");

                    b.Property<int>("PurchaseOrderItemStatus")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<decimal>("TotalCostPrice")
                        .HasPrecision(10, 2)
                        .HasColumnType("numeric(10,2)");

                    b.Property<decimal>("TotalQuantity")
                        .HasPrecision(10, 3)
                        .HasColumnType("numeric(10,3)");

                    b.HasKey("Id");

                    b.HasIndex("MasterProductId");

                    b.HasIndex("PurchaseOrderId");

                    b.ToTable("PurchaseOrderItems");
                });

            modelBuilder.Entity("CA.ERP.DataAccess.Entities.Stock", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BranchId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("CostPrice")
                        .HasPrecision(18, 2)
                        .HasColumnType("numeric(18,2)");

                    b.Property<Guid>("MasterProductId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("PurchaseOrderItemId")
                        .HasColumnType("uuid");

                    b.Property<string>("SerialNumber")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("StockNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<Guid>("StockReceiveId")
                        .HasColumnType("uuid");

                    b.Property<int>("StockStatus")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.HasIndex("MasterProductId");

                    b.HasIndex("PurchaseOrderItemId");

                    b.HasIndex("SerialNumber")
                        .IsUnique();

                    b.HasIndex("StockNumber")
                        .IsUnique();

                    b.HasIndex("StockReceiveId");

                    b.ToTable("Stocks");
                });

            modelBuilder.Entity("CA.ERP.DataAccess.Entities.StockCounter", b =>
                {
                    b.Property<string>("Code")
                        .HasMaxLength(3)
                        .HasColumnType("character varying(3)");

                    b.Property<int>("Counter")
                        .HasColumnType("integer");

                    b.Property<int>("Year")
                        .HasColumnType("integer");

                    b.HasKey("Code");

                    b.ToTable("StockCounters");
                });

            modelBuilder.Entity("CA.ERP.DataAccess.Entities.StockReceive", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BranchId")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset?>("DateReceived")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DeliveryReference")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<Guid?>("PurchaseOrderId")
                        .HasColumnType("uuid");

                    b.Property<int>("Stage")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<int>("StockSouce")
                        .HasColumnType("integer");

                    b.Property<Guid>("SupplierId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.HasIndex("DateCreated");

                    b.HasIndex("PurchaseOrderId");

                    b.HasIndex("SupplierId");

                    b.ToTable("StockReceives");
                });

            modelBuilder.Entity("CA.ERP.DataAccess.Entities.StockReceiveItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BranchId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("CostPrice")
                        .HasPrecision(18, 2)
                        .HasColumnType("numeric(18,2)");

                    b.Property<Guid>("MasterProductId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("PurchaseOrderItemId")
                        .HasColumnType("uuid");

                    b.Property<string>("SerialNumber")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("StockNumber")
                        .HasColumnType("text");

                    b.Property<Guid>("StockReceiveId")
                        .HasColumnType("uuid");

                    b.Property<int>("StockStatus")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.HasIndex("MasterProductId");

                    b.HasIndex("PurchaseOrderItemId");

                    b.HasIndex("StockReceiveId");

                    b.ToTable("StockReceiveItems");
                });

            modelBuilder.Entity("CA.ERP.DataAccess.Entities.Supplier", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("ContactPerson")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("CA.ERP.DataAccess.Entities.SupplierBrand", b =>
                {
                    b.Property<Guid>("SupplierId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("BrandId")
                        .HasColumnType("uuid");

                    b.HasKey("SupplierId", "BrandId");

                    b.HasIndex("BrandId");

                    b.ToTable("SupplierBrands");
                });

            modelBuilder.Entity("CA.ERP.DataAccess.Entities.SupplierMasterProduct", b =>
                {
                    b.Property<Guid>("SupplierId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("MasterProductId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("CostPrice")
                        .HasPrecision(18, 2)
                        .HasColumnType("numeric(18,2)");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("SupplierId", "MasterProductId");

                    b.HasIndex("MasterProductId");

                    b.ToTable("SupplierMasterProducts");
                });

            modelBuilder.Entity("CA.ERP.DataAccess.Entities.MasterProduct", b =>
                {
                    b.HasOne("CA.ERP.DataAccess.Entities.Brand", "Brand")
                        .WithMany("MasterProducts")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Brand");
                });

            modelBuilder.Entity("CA.ERP.DataAccess.Entities.PurchaseOrder", b =>
                {
                    b.HasOne("CA.ERP.DataAccess.Entities.Branch", "Branch")
                        .WithMany("PurchaseOrders")
                        .HasForeignKey("DestinationBranchId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("CA.ERP.DataAccess.Entities.Supplier", "Supplier")
                        .WithMany("PurchaseOrders")
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Branch");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("CA.ERP.DataAccess.Entities.PurchaseOrderItem", b =>
                {
                    b.HasOne("CA.ERP.DataAccess.Entities.MasterProduct", "MasterProduct")
                        .WithMany()
                        .HasForeignKey("MasterProductId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("CA.ERP.DataAccess.Entities.PurchaseOrder", "PurchaseOrder")
                        .WithMany("PurchaseOrderItems")
                        .HasForeignKey("PurchaseOrderId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("MasterProduct");

                    b.Navigation("PurchaseOrder");
                });

            modelBuilder.Entity("CA.ERP.DataAccess.Entities.Stock", b =>
                {
                    b.HasOne("CA.ERP.DataAccess.Entities.Branch", "Branch")
                        .WithMany("Stocks")
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("CA.ERP.DataAccess.Entities.MasterProduct", "MasterProduct")
                        .WithMany("Stocks")
                        .HasForeignKey("MasterProductId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("CA.ERP.DataAccess.Entities.PurchaseOrderItem", "PurchaseOrderItem")
                        .WithMany("Stocks")
                        .HasForeignKey("PurchaseOrderItemId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("CA.ERP.DataAccess.Entities.StockReceive", "StockReceive")
                        .WithMany("Stocks")
                        .HasForeignKey("StockReceiveId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Branch");

                    b.Navigation("MasterProduct");

                    b.Navigation("PurchaseOrderItem");

                    b.Navigation("StockReceive");
                });

            modelBuilder.Entity("CA.ERP.DataAccess.Entities.StockReceive", b =>
                {
                    b.HasOne("CA.ERP.DataAccess.Entities.Branch", "Branch")
                        .WithMany("StockReceives")
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("CA.ERP.DataAccess.Entities.PurchaseOrder", "PurchaseOrder")
                        .WithMany("StockReceives")
                        .HasForeignKey("PurchaseOrderId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("CA.ERP.DataAccess.Entities.Supplier", "Supplier")
                        .WithMany("StockReceives")
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Branch");

                    b.Navigation("PurchaseOrder");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("CA.ERP.DataAccess.Entities.StockReceiveItem", b =>
                {
                    b.HasOne("CA.ERP.DataAccess.Entities.Branch", "Branch")
                        .WithMany("StockReceiveItems")
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("CA.ERP.DataAccess.Entities.MasterProduct", "MasterProduct")
                        .WithMany("StockReceiveItems")
                        .HasForeignKey("MasterProductId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("CA.ERP.DataAccess.Entities.PurchaseOrderItem", "PurchaseOrderItem")
                        .WithMany("StockReceiveItems")
                        .HasForeignKey("PurchaseOrderItemId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("CA.ERP.DataAccess.Entities.StockReceive", "StockReceive")
                        .WithMany("Items")
                        .HasForeignKey("StockReceiveId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Branch");

                    b.Navigation("MasterProduct");

                    b.Navigation("PurchaseOrderItem");

                    b.Navigation("StockReceive");
                });

            modelBuilder.Entity("CA.ERP.DataAccess.Entities.SupplierBrand", b =>
                {
                    b.HasOne("CA.ERP.DataAccess.Entities.Brand", "Brand")
                        .WithMany("SupplierBrands")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CA.ERP.DataAccess.Entities.Supplier", "Supplier")
                        .WithMany("SupplierBrands")
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("CA.ERP.DataAccess.Entities.SupplierMasterProduct", b =>
                {
                    b.HasOne("CA.ERP.DataAccess.Entities.MasterProduct", "MasterProduct")
                        .WithMany("SupplierMasterProducts")
                        .HasForeignKey("MasterProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CA.ERP.DataAccess.Entities.Supplier", null)
                        .WithMany("SupplierMasterProducts")
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MasterProduct");
                });

            modelBuilder.Entity("CA.ERP.DataAccess.Entities.Branch", b =>
                {
                    b.Navigation("PurchaseOrders");

                    b.Navigation("StockReceiveItems");

                    b.Navigation("StockReceives");

                    b.Navigation("Stocks");
                });

            modelBuilder.Entity("CA.ERP.DataAccess.Entities.Brand", b =>
                {
                    b.Navigation("MasterProducts");

                    b.Navigation("SupplierBrands");
                });

            modelBuilder.Entity("CA.ERP.DataAccess.Entities.MasterProduct", b =>
                {
                    b.Navigation("StockReceiveItems");

                    b.Navigation("Stocks");

                    b.Navigation("SupplierMasterProducts");
                });

            modelBuilder.Entity("CA.ERP.DataAccess.Entities.PurchaseOrder", b =>
                {
                    b.Navigation("PurchaseOrderItems");

                    b.Navigation("StockReceives");
                });

            modelBuilder.Entity("CA.ERP.DataAccess.Entities.PurchaseOrderItem", b =>
                {
                    b.Navigation("StockReceiveItems");

                    b.Navigation("Stocks");
                });

            modelBuilder.Entity("CA.ERP.DataAccess.Entities.StockReceive", b =>
                {
                    b.Navigation("Items");

                    b.Navigation("Stocks");
                });

            modelBuilder.Entity("CA.ERP.DataAccess.Entities.Supplier", b =>
                {
                    b.Navigation("PurchaseOrders");

                    b.Navigation("StockReceives");

                    b.Navigation("SupplierBrands");

                    b.Navigation("SupplierMasterProducts");
                });
#pragma warning restore 612, 618
        }
    }
}
