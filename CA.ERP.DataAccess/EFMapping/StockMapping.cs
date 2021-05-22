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
    public class StockMapping : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.HasKey(t => t.Id);

            builder.HasIndex(t => t.StockNumber).IsUnique();
            builder.HasIndex(t => t.SerialNumber).IsUnique();

            builder.Property(t => t.StockNumber).IsRequired().HasMaxLength(50);
            builder.Property(t => t.SerialNumber).HasMaxLength(50);
            builder.Property(t => t.CostPrice).HasPrecision(18, 2);

            builder.HasOne(t => t.MasterProduct)
                .WithMany(t => t.Stocks)
                .HasForeignKey(t => t.MasterProductId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(t => t.Supplier)
                .WithMany(t => t.Stocks)
                .HasForeignKey(t => t.SupplierId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(t => t.Branch)
                .WithMany(t => t.Stocks)
                .HasForeignKey(t => t.BranchId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
