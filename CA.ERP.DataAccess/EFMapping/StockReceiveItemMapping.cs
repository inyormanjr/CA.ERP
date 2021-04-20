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
    public class StockReceiveItemMapping : IEntityTypeConfiguration<StockReceiveItem>
    {
        public void Configure(EntityTypeBuilder<StockReceiveItem> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.SerialNumber).HasMaxLength(50);
            builder.Property(t => t.CostPrice).HasPrecision(18, 2);

            builder.HasOne(t => t.MasterProduct)
                .WithMany(t => t.StockReceiveItems)
                .HasForeignKey(t => t.MasterProductId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(t => t.StockReceive)
                .WithMany(t => t.Items)
                .HasForeignKey(t => t.StockReceiveId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(t => t.PurchaseOrderItem)
                .WithMany(t => t.StockReceiveItems)
                .HasForeignKey(t => t.PurchaseOrderItemId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(t => t.Branch)
                .WithMany(t => t.StockReceiveItems)
                .HasForeignKey(t => t.BranchId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
