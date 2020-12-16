
using CA.ERP.Lib.DAL.FluentApiMapping;
using CA.ERP.Lib.Domain.BranchAgg;
using CA.ERP.Lib.Domain.UserAgg;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Lib.DAL
{
    public class CADataContext: DbContext
    {
       public CADataContext(DbContextOptions options): base(options)
        {

        }

       public  DbSet<Branch> Branches { get; set; }
       public  DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new BranchMapping());
            builder.ApplyConfiguration(new UserMapping());
            base.OnModelCreating(builder);
        }
    }
}
