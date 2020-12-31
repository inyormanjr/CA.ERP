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
    public class SupplierBrandMapping : IEntityTypeConfiguration<SupplierBrand>
    {
        public void Configure(EntityTypeBuilder<SupplierBrand> builder)
        {
            builder.HasKey(t => new { t.SupplierId, t.BrandId });

            builder.HasOne(t => t.Brand)
                .WithMany(t => t.SupplierBrands)
                .HasForeignKey(t => t.BrandId);
        }
    }
}
