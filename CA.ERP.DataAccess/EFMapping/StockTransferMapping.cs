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
    public class StockTransferMapping : IEntityTypeConfiguration<StockTransfer>
    {
        public void Configure(EntityTypeBuilder<StockTransfer> builder)
        {
            builder.HasKey(t => t.Id);

            builder.HasIndex(t => t.CreatedAt);

            builder.HasOne(t => t.SourceBranch)
                .WithMany()
                .HasForeignKey(t => t.SourceBranchId);

            builder.HasOne(t => t.DestinationBranch)
                .WithMany()
                .HasForeignKey(t => t.DestinationBranchId);
        }
    }
}
