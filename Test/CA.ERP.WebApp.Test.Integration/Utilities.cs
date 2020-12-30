using Bogus;
using CA.ERP.DataAccess;
using CA.ERP.DataAccess.Entities;
using CA.ERP.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Test.Integration
{
    public static class Utilities
    {
        private static object _lock = new object();
        /// <summary>
        /// default pupulation for the database to be use in testing.
        /// </summary>
        /// <param name="db">The db context</param>
        public static void InitializeDbForTests(CADataContext db, PasswordManagementHelper passwordManagementHelper)
        {
            lock (_lock)
            {
                var existingBranchIdFlag = Guid.Parse("56e5e4fc-c583-4186-a288-55392a6946d4");
                if (db.Branches.Any(b => b.Id == existingBranchIdFlag))
                {
                    return;
                }

                var fakeBranchGenerator = new Faker<Branch>()
                    .CustomInstantiator(f => new Branch() { Id = Guid.NewGuid() })
                    .RuleFor(f => f.Name, f => f.Address.City())
                    .RuleFor(f => f.BranchNo, f => f.PickRandom<int>(1, 2, 3, 4, 5))
                    .RuleFor(f => f.Code, f => f.PickRandom<int>(1, 2, 3, 4, 5).ToString("00000"))
                    .RuleFor(f => f.Address, f => f.Address.StreetAddress())
                    .RuleFor(f => f.Contact, f => f.Name.FullName());

                //add branch for testing
                Branch branch = fakeBranchGenerator.Generate();
                for (int i = 0; i < 10; i++)
                {
                    branch = fakeBranchGenerator.Generate();
                    if (i == 0)
                    {
                        branch.Id = Guid.Parse("56e5e4fc-c583-4186-a288-55392a6946d4");
                    }
                    else if (i == 1)
                    {
                        branch.Id = Guid.Parse("e80554e8-e7b5-4f8c-8e59-9d612d547d02");
                    }
                    else if (i == 2)
                    {
                        branch.Id = Guid.Parse("f853efb7-9aec-4750-bbcc-dbfd1ae47063");
                    }
                    db.Branches.Add(branch);
                }
                
                db.SaveChanges();

                for (int i = 0; i < 10; i++)
                {
                    //add user for login.
                    string password = "password";
                    passwordManagementHelper.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

                    var suffix = i == 0 ? string.Empty : i.ToString();

                    var user = new User() { Id = Guid.NewGuid() , Username = "ExistingUser" + suffix, Role = UserRole.Admin, FirstName = "Existing" , LastName = "User" };
                    user.SetHashAndSalt(passwordHash, passwordSalt);

                    if (i == 0)
                    {
                        user.Id = Guid.Parse("9e3205dc-f63d-49b3-bbc3-67ccf15e3ffa");
                    }
                    else if (i == 1)
                    {
                        user.Id = Guid.Parse("14a2497c-f85d-40cb-9361-92a580b1b6c5");
                    }
                    else if (i == 2)
                    {
                        user.Id = Guid.Parse("e02fbc42-a8dc-4359-bfa1-7f0774bd1fd4");
                    }

                    user.UserBranches.Add(new UserBranch() { BranchId = branch.Id, UserId = user.Id, Branch = branch, User = user });

                    db.Users.Add(user);
                }

                //add suppliers
                var fakeSupplierGenerator = new Faker<Supplier>()
                    .CustomInstantiator(f => new Supplier())
                    .RuleFor(f => f.Name, f => f.Company.CompanyName())
                    .RuleFor(f => f.Address, f => f.Address.StreetAddress())
                    .RuleFor(f => f.ContactPerson, f => f.Name.FullName());

                var supplier = fakeSupplierGenerator.Generate();
                supplier.Id = Guid.Parse("25c38e11-0929-43f4-993d-76ab5ddba3f1");

                db.Suppliers.Add(supplier);

                //more supplier
                for (int i = 0; i < 10; i++)
                {
                    db.Suppliers.Add(fakeSupplierGenerator.Generate());
                }


                //add brands
                var fakeBrandGenerator = new Faker<Brand>()
                    .RuleFor(f => f.Name, f => f.Company.CompanyName())
                    .RuleFor(f => f.Description, f => f.Company.CatchPhrase());

                for (int i = 0; i < 10; i++)
                {
                    var brand = fakeBrandGenerator.Generate();
                    if (i == 0)
                    {
                        brand.Id = Guid.Parse("4f724f6a-e590-41a7-96e1-b9d64febaa4c");
                    }
                    else if (i == 1)
                    {
                        brand.Id = Guid.Parse("4d2cfc04-ed36-433f-8053-a5eefce5bb2d");
                    }
                    db.Brands.Add(brand);
                }

                var fakeMasterProductGenerator = new Faker<MasterProduct>()
                    .RuleFor(f => f.Model, f => f.Vehicle.Model())
                    .RuleFor(f => f.Description, f => f.Vehicle.Manufacturer())
                    .RuleFor(f => f.BrandId, f => Guid.Parse("4d2cfc04-ed36-433f-8053-a5eefce5bb2d"));


                for (int i = 0; i < 10; i++)
                {
                    var masterProduct = fakeMasterProductGenerator.Generate();
                    if (i == 0)
                    {
                        masterProduct.Id = Guid.Parse("78d75126-c24d-48d5-a192-f06db4ff6df3");
                    }
                    else if (i == 1)
                    {
                        masterProduct.Id = Guid.Parse("f17db084-0b01-4226-b3c0-95d1953075ef");
                    }

                    db.MasterProducts.Add(masterProduct);
                }

                db.SaveChanges();
            }
        }
    }
}
