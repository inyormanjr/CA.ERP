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
    public class StockCounterMapping : IEntityTypeConfiguration<StockCounter>
    {
        public void Configure(EntityTypeBuilder<StockCounter> builder)
        {
            builder.HasKey(t => t.Code);
            builder.Property(t => t.Code).HasMaxLength(3).IsRequired();
        }
    }
}
