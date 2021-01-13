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
    public class StockInventoryMapping : IEntityTypeConfiguration<StockInventory>
    {
        public void Configure(EntityTypeBuilder<StockInventory> builder)
        {
            builder.HasKey(t=> new { t.BranchId, t.MasterProductId});

            builder.Property(t => t.Quantity)
                .HasPrecision(18, 3);

            builder.HasOne(t => t.Branch)
                .WithMany(t => t.StockInventories)
                .HasForeignKey(t => t.BranchId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.MasterProduct)
                .WithMany(t => t.StockInventories)
                .HasForeignKey(t => t.MasterProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
