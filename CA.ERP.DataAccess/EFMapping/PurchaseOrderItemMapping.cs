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
    public class PurchaseOrderItemMapping : IEntityTypeConfiguration<PurchaseOrderItem>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrderItem> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()").ValueGeneratedOnAdd();
            builder.Property(t => t.OrderedQuantity).HasPrecision(10, 3);
            builder.Property(t => t.FreeQuantity).HasPrecision(10, 3);
            builder.Property(t => t.TotalQuantity).HasPrecision(10, 3);
            builder.Property(t => t.DeliveredQuantity).HasPrecision(10, 3);

            builder.Property(t => t.CostPrice).HasPrecision(10, 2);
            builder.Property(t => t.Discount).HasPrecision(10, 2);
            builder.Property(t => t.TotalCostPrice).HasPrecision(10, 2);


            builder.HasOne(x => x.PurchaseOrder)
                .WithMany(x => x.PurchaseOrderItems)
                .HasForeignKey(x => x.PurchaseOrderId);

            builder.HasOne(x => x.MasterProduct)
                .WithMany()
                .HasForeignKey(x => x.MasterProductId);
        }
    }
}
