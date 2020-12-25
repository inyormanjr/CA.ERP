
using CA.ERP.DataAccess.EFMapping;
using CA.ERP.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.DataAccess
{
    public class CADataContext: DbContext
    {
       public CADataContext(DbContextOptions options): base(options)
        {

        }

       public  DbSet<Branch> Branches { get; set; }
       public  DbSet<User> Users { get; set; }
       public  DbSet<UserBranch> UserBranches { get; set; }
       public  DbSet<Supplier> Suppliers { get; set; }
       public  DbSet<SupplierBrand> SupplierBrands { get; set; }
       public  DbSet<Brand> Brands { get; set; }
       public  DbSet<MasterProduct> MasterProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new BranchMapping());
            builder.ApplyConfiguration(new UserMapping());
            builder.ApplyConfiguration(new UserBranchMapping());
            builder.ApplyConfiguration(new SupplierMapping());
            builder.ApplyConfiguration(new SupplierBrandMapping());
            builder.ApplyConfiguration(new BrandMapping());
            builder.ApplyConfiguration(new MasterProductMapping());
            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            onBeforeSaveChanges();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            onBeforeSaveChanges();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void onBeforeSaveChanges()
        {
            var entries = ChangeTracker.Entries<EntityBase>();
            var utcNow = DateTime.UtcNow;

            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        // set the updated date to "now"
                        entry.Entity.UpdatedAt = utcNow;

                        // mark property as "don't touch"
                        // we don't want to update on a Modify operation
                        entry.Property("CreatedAt").IsModified = false;
                        break;

                    case EntityState.Added:
                        // set both updated and created date to "now"
                        entry.Entity.CreatedAt = utcNow;
                        entry.Entity.UpdatedAt = utcNow;
                        break;
                }
            }
        }

    }
}
