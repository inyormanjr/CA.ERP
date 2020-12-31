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
    public class SupplierMasterProductMapping : IEntityTypeConfiguration<SupplierMasterProduct>
    {
        public void Configure(EntityTypeBuilder<SupplierMasterProduct> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()").ValueGeneratedOnAdd();
            builder.Property(x => x.CostPrice).HasPrecision(18, 2);

            builder.HasOne(t => t.MasterProduct)
                .WithMany(t=>t.SupplierMasterProducts)
                .HasForeignKey(t => t.MasterProductId);

            builder.HasOne(t => t.Supplier)
                .WithMany(t => t.SupplierMasterProducts)
                .HasForeignKey(t => t.SupplierId);

        }
    }
}
