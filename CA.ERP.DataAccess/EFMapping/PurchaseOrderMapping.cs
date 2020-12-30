using CA.ERP.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.DataAccess.EFMapping
{
    public class PurchaseOrderMapping : IEntityTypeConfiguration<PurchaseOrder>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrder> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasDefaultValueSql("NEWID()").ValueGeneratedOnAdd();
            builder.HasIndex(t => t.Barcode).IsUnique(true);

            builder.Property(t => t.Barcode).HasMaxLength(20);
            builder.Property(t => t.TotalCostPrice).HasPrecision(10,2);
            

            builder.HasOne(t => t.Supplier)
                .WithMany()
                .HasForeignKey(t => t.SupplierId);

            builder.HasOne(t => t.Branch)
                .WithMany()
                .HasForeignKey(t => t.BranchId);

            builder.HasOne(t => t.ApprovedBy)
                .WithMany()
                .HasForeignKey(t => t.ApprovedById);
        }
    }
}
