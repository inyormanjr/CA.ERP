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
    class ChequePaymentDetailMapping : IEntityTypeConfiguration<ChequePaymentDetail>
    {
        public void Configure(EntityTypeBuilder<ChequePaymentDetail> builder)
        {
            builder.HasKey(t => t.PaymentId);
            builder.HasIndex(t => t.ChequeNumber);
            builder.Property(t => t.ChequeNumber).IsRequired().HasMaxLength(50);

            builder.HasOne(t => t.Payment)
                .WithOne(t => t.ChequePaymentDetail)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.Bank)
                .WithMany(t => t.ChequePaymentDetails)
                .HasForeignKey(t => t.BankId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
