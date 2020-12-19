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
        /// <summary>
        /// default pupulation for the database to be use in testing.
        /// </summary>
        /// <param name="db">The db context</param>
        public static void InitializeDbForTests(CADataContext db, PasswordManagementHelper passwordManagementHelper)
        {
            //add user for login.
            string password = "password";
            passwordManagementHelper.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User() { Id = Guid.NewGuid().ToString(), Username = "ExistingUser", BranchId = 1 };
            user.SetHashAndSalt(passwordHash, passwordSalt);
            db.Users.Add(user);

            var fakeBranchGenerator = new Faker<Branch>()
                .CustomInstantiator(f => new Branch() { Id = "56e5e4fc-c583-4186-a288-55392a6946d4" })
                .RuleFor(f => f.Name, f => f.Address.City())
                .RuleFor(f => f.BranchNo, f => f.PickRandom<int>(1,2,3,4,5))
                .RuleFor(f => f.Code, f => f.PickRandom<int>(1, 2, 3, 4, 5).ToString("00000"))
                .RuleFor(f => f.Address, f => f.Address.StreetAddress())
                .RuleFor(f => f.Contact, f => f.Name.FullName());

            //add branch for testing
            db.Branches.Add(fakeBranchGenerator.Generate());

            db.SaveChanges();
        }
    }
}
