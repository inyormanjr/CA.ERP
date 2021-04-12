using CA.ERP.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.DataAccess.EFMapping
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
      builder.Property(x => x.Id);
            builder.HasIndex(x => x.Username).IsUnique(true);

            builder.HasMany(x => x.UserBranches)
                .WithOne(x => x.User)
                .HasForeignKey(x=>x.UserId);
 
        }
    }
}
