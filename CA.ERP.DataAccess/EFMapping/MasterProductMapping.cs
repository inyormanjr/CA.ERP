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
    public class MasterProductMapping : IEntityTypeConfiguration<MasterProduct>
    {
        public void Configure(EntityTypeBuilder<MasterProduct> builder)
        {
            builder.HasKey(t => t.Id);
            builder.HasIndex(t => t.Model).IsUnique(true);
            builder.Property(t => t.Model).HasMaxLength(100);
            builder.Property(t => t.Description).HasMaxLength(200);

            builder.HasOne(t => t.Brand)
                .WithMany(t => t.MasterProducts)
                .HasForeignKey(t => t.BrandId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
