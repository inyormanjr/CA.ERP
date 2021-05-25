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
    public class StockTransferItemMapping : IEntityTypeConfiguration<StockTransferItem>
    {
        public void Configure(EntityTypeBuilder<StockTransferItem> builder)
        {
            builder.HasKey(t => t.Id);

            builder.HasOne(t => t.MasterProduct)
                .WithMany()
                .HasForeignKey(t => t.MasterProductId);

            builder.HasOne(t => t.StockTransfer)
                .WithMany(t=>t.Items)
                .HasForeignKey(t => t.StockTransferId);

        }
    }
}
