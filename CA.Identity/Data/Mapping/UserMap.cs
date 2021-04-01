using CA.Identity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.Identity.Data.Mapping
{
    public class UserMap : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasMany(t => t.UserBranches)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId);
        }
    }
}
