using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.Identity.Data.Mapping
{
    public class UserBranchMap : IEntityTypeConfiguration<UserBranch>
    {
        public void Configure(EntityTypeBuilder<UserBranch> builder)
        {
            builder.HasKey(t => new { t.UserId, t.BranchId });
        }
    }
}
