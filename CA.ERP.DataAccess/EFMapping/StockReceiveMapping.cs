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
    public class StockReceiveMapping : IEntityTypeConfiguration<StockReceive>
    {


        public void Configure(EntityTypeBuilder<StockReceive> builder)
        {
            builder.HasKey(t => t.Id);

            builder.HasIndex(t => t.DateCreated);

            builder.Property(t => t.DeliveryReference).HasMaxLength(50);
            

            builder.HasOne(t => t.PurchaseOrder)
                .WithMany(t => t.StockReceives)
                .HasForeignKey(t => t.PurchaseOrderId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(t => t.Branch)
                .WithMany(t => t.StockReceives)
                .HasForeignKey(t => t.BranchId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.HasMany(t => t.Items)
                .WithOne(t => t.StockReceive)
                .HasForeignKey(t => t.StockReceiveId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(t => t.Supplier)
                .WithMany(t => t.StockReceives)
                .HasForeignKey(t => t.SupplierId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
