using Bogus;
using CA.ERP.DataAccess;
using CA.ERP.DataAccess.Entities;
using CA.ERP.Domain.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace CA.ERP.Utilities
{
    class Program
    {
        static void Main(string[] args)
        {
            Seed();
            if (args[0] == "/seed")
            {
                
            }
        }

        public static IConfigurationRoot GetConfiguration()
        {
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.json");
            configurationBuilder.AddJsonFile("appsettings.local.json");

            return configurationBuilder.Build();
        }

        public static void Seed()
        {
            Console.WriteLine("Seeding database");

            var connectionstring = GetConfiguration().GetSection("connectionStrings")["DefaultConnection"];

            var optionsBuilder = new DbContextOptionsBuilder<CADataContext>();
            optionsBuilder.UseSqlServer(connectionstring);

            var passwordManagementHelper = new PasswordManagementHelper();

            using (var db = new CADataContext(options: optionsBuilder.Options))
            {
                //create database not present else do nothing;
                db.Database.Migrate();
                var branches = generateBranch(10).ToList();
                db.Branches.AddRange(branches);

                //add user for login.
                var users = generateUsers(db, passwordManagementHelper, branches);

                db.Users.AddRange(users);


                var brands = generateBrands().ToList();

                db.Brands.AddRange(brands);

                db.SaveChanges();

                var suppliers = generateSupplier(brands);

                db.Suppliers.AddRange(suppliers);

                var masterProducts = generateMasterProduct(brands);

                db.MasterProducts.AddRange(masterProducts);

                db.SaveChanges();
            }

            Console.WriteLine("Seeding done");
            Console.WriteLine("Press enter to contine");
            Console.ReadLine();

        }

        private static IEnumerable<User> generateUsers(CADataContext dataContext, PasswordManagementHelper passwordManagementHelper, List<Branch> branches, int count = 5)
        {
           

            var fakeUserGenerator = new Faker<User>()
                            .RuleFor(f => f.Username, f => f.Internet.UserName())
                            .RuleFor(f => f.FirstName, f => f.Name.FirstName())
                            .RuleFor(f => f.LastName, f => f.Name.LastName());



            for (int i = 0; i < count; i++)
            {
                var user = fakeUserGenerator.Generate();
                Random random = new Random();

                const int mask = 127;
                var roles = (mask & (random.Next(mask) + 1));
                user.Role = (UserRole)roles;

                if (i == 0)
                {
                    if (!dataContext.Users.Any(u => u.Username == "Admin"))
                    {
                        user.Username = "Admin";
                        user.Role = UserRole.Admin;
                    }
                }

                string password = "password";
                passwordManagementHelper.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
                user.SetHashAndSalt(passwordHash, passwordSalt);

                //random branch count
                
                var numberOfBranch = random.Next(0, branches.Count - 1);
                for (int x = 0; x < numberOfBranch; x++)
                {
                    var branch = branches[x];
                    if (!user.UserBranches.Any(ub => ub.BranchId == branch.Id))
                    {
                        user.UserBranches.Add(new UserBranch() { BranchId = branch.Id, UserId = user.Id });
                    }
                }

                
                yield return user;
            }
            
        }

        private static IEnumerable<Branch> generateBranch(int count = 10)
        {
            var fakeBranchGenerator = new Faker<Branch>()
                            .CustomInstantiator(f => new Branch() { Id = Guid.NewGuid() })
                            .RuleFor(f => f.Name, f => f.Address.City())
                            .RuleFor(f => f.BranchNo, f => f.PickRandom<int>(1, 2, 3, 4, 5))
                            .RuleFor(f => f.Code, f => f.PickRandom<int>(1, 2, 3, 4, 5).ToString("00000"))
                            .RuleFor(f => f.Address, f => f.Address.StreetAddress())
                            .RuleFor(f => f.Contact, f => f.Name.FullName());

            for (int i = 0; i < count; i++)
            {
                yield return fakeBranchGenerator.Generate();
            }

        }

        private static IEnumerable<Brand> generateBrands( int count = 10)
        {
            var brands = new List<Brand>();
            var fakeBrandGenerator = new Faker<Brand>()
                            .RuleFor(f => f.Name, f => f.Vehicle.Manufacturer() + " " + DateTimeOffset.Now.ToUnixTimeMilliseconds())
                            .RuleFor(f => f.Description, f => f.Commerce.ProductDescription());


            for (int i = 0; i < count; i++)
            {
                Thread.Sleep(1);
                yield return fakeBrandGenerator.Generate();
            }
        }

        private static IEnumerable<Supplier> generateSupplier(List<Brand> brands, int count = 10)
        {
            var fakeSupplierGenerator = new Faker<Supplier>()
                .CustomInstantiator( f => new Supplier() { Id = Guid.NewGuid()})
                            .RuleFor(f => f.Name, f => f.Company.CompanyName())
                            .RuleFor(f => f.Name, f => f.Company.CatchPhrase());

            var random = new Random();

            for (int i = 0; i < count; i++)
            {
                var supplier = fakeSupplierGenerator.Generate();
                var brandIds = brands.OrderBy(b => random.Next()).Take(5).Select(b => b.Id);
                foreach (var brandId in brandIds)
                {
                    supplier.SupllierBrands.Add(new SupplierBrand() { BrandId = brandId, SupplierId = supplier.Id });
                }

                yield return supplier;
            }

        }


        private static IEnumerable<MasterProduct> generateMasterProduct(List<Brand> brands, int count = 10)
        {
            var fakeMasterProductGenerator = new Faker<MasterProduct>()
                            .RuleFor(f => f.Model, f => f.Vehicle.Model() + " - " + DateTimeOffset.Now.ToUnixTimeMilliseconds())
                            .RuleFor(f => f.Description, f => f.Vehicle.Manufacturer());
            foreach (var brand in brands)
            {
                for (int i = 0; i < count; i++)
                {
                    Thread.Sleep(1);
                    var masterProduct = fakeMasterProductGenerator.Generate();
                    masterProduct.BrandId = brand.Id;
                    yield return masterProduct;
                }
            }
        }
    }
}
