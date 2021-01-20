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
    class CardPaymentDetailMapping : IEntityTypeConfiguration<CardPaymentDetail>
    {
        public void Configure(EntityTypeBuilder<CardPaymentDetail> builder)
        {
            builder.HasKey(t => t.PaymentId);
            builder.HasIndex(t => t.TransactionNumber).IsUnique(true);
            builder.Property(t => t.TransactionNumber).IsRequired().HasMaxLength(50);

            builder.HasOne(t => t.Payment)
                .WithOne(t => t.CardPaymentDetail)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.Bank)
                .WithMany(t => t.CardPaymentDetails)
                .HasForeignKey(t=>t.BankId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
