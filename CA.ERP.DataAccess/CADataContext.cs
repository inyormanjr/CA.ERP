
using CA.ERP.DataAccess.Entities;
using CA.ERP.Lib.DAL.EFMapping;
using Microsoft.EntityFrameworkCore;

namespace CA.ERP.DataAccess
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
