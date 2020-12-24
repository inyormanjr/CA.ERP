using Bogus;
using CA.ERP.DataAccess;
using CA.ERP.DataAccess.Entities;
using CA.ERP.Domain.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CA.ERP.Utilities
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args[0] == "/seed")
            {
                Seed();
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
                var users = generateUsers(passwordManagementHelper, branches);

                db.Users.AddRange(users);

                db.SaveChanges();
            }

            Console.WriteLine("Seeding done");
            Console.WriteLine("Press enter to contine");
            Console.ReadLine();

        }

        private static IEnumerable<User> generateUsers(PasswordManagementHelper passwordManagementHelper, List<Branch> branches, int count = 5)
        {
           

            var fakeUserGenerator = new Faker<User>()
                            .RuleFor(f => f.Username, f => f.Internet.UserName())
                            .RuleFor(f => f.FirstName, f => f.Name.FirstName())
                            .RuleFor(f => f.LastName, f => f.Name.LastName());



            for (int i = 0; i < count; i++)
            {
                var user = fakeUserGenerator.Generate();
                Random random = new Random();

                user.Username = i == 0 ? "Admin" : user.Username;


                const int mask = 127;
                var roles = (mask & (random.Next(mask) + 1));

                user.Role = i == 0 ? UserRole.Admin : (UserRole)roles;

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
    }
}
