using CA.ERP.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.DataAccess.EFMapping
{
    class BranchMapping : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.HasKey(x => x.Id);
      builder.Property(x => x.Id);
            builder.HasIndex(x => x.Name).IsUnique(true);

            
        }
    }
}
