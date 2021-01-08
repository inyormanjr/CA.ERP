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

            builder.Property(t => t.DeliveryReference).HasMaxLength(50);

            builder.HasOne(t => t.PurchaseOrder)
                .WithMany(t => t.StockReceives)
                .HasForeignKey(t => t.PurchaseOrderId);

            builder.HasOne(t => t.Branch)
                .WithMany(t => t.StockReceives)
                .HasForeignKey(t => t.BranchId);


            builder.HasMany(t => t.Stocks)
                .WithOne(t => t.StockReceive)
                .HasForeignKey(t => t.StockReceiveId);
        }
    }
}
