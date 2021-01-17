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
    public class CashPaymentDetailMapping : IEntityTypeConfiguration<CashPaymentDetail>
    {
        public void Configure(EntityTypeBuilder<CashPaymentDetail> builder)
        {
            builder.HasKey(t => t.PaymentId);

            builder.Property(t => t.Change).HasPrecision(18, 2);
            builder.Property(t => t.TenderAmount).HasPrecision(18, 2);

            builder.HasOne(t => t.Payment)
                .WithOne(t => t.CashPaymentDetail)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
