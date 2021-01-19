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
    public class PaymentMapping : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasIndex(t => t.Id);
            builder.HasIndex(t => t.OfficialReceiptNumber).IsUnique(true);

            builder.Property(t => t.OfficialReceiptNumber).IsRequired().HasMaxLength(20);

            builder.Property(t => t.GrossAmount).HasPrecision(18, 2);
            builder.Property(t => t.Rebate).HasPrecision(18, 2);
            builder.Property(t => t.Interest).HasPrecision(18, 2);
            builder.Property(t => t.Discount).HasPrecision(18, 2);
            builder.Property(t => t.NetAmount).HasPrecision(18, 2);

            builder.HasOne(t => t.Transaction)
                .WithMany(t => t.Payments)
                .HasForeignKey(t=>t.TransactionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
