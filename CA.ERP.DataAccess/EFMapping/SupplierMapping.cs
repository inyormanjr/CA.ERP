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
    public class SupplierMapping : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.HasKey(t => t.Id);
            builder.HasIndex(t => t.Name).IsUnique(true);
            builder.Property(t => t.Name).HasMaxLength(100);
            builder.Property(t => t.Address).HasMaxLength(200);
            builder.Property(t => t.Contact).HasMaxLength(100);

            builder.HasMany(t => t.SupllierBrands)
                .WithOne(t => t.Supplier)
                .HasForeignKey(t => t.SupplierId);

        }
    }
}
