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

            builder.Property(t => t.StockNumber).HasMaxLength(50);
            builder.Property(t => t.SerialNumber).HasMaxLength(50);
            builder.Property(t => t.CostPrice).HasPrecision(18, 2);

            builder.HasOne(t => t.MasterProduct)
                .WithMany(t => t.Stocks)
                .HasForeignKey(t => t.MasterProductId);

            builder.HasOne(t => t.StockReceive)
                .WithMany(t => t.Stocks)
                .HasForeignKey(t => t.StockReceiveId);

            builder.HasOne(t => t.PurchaseOrderItem)
                .WithMany(t => t.Stocks)
                .HasForeignKey(t => t.PurchaseOrderItemId);
        }
    }
}
