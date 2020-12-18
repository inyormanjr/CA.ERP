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

            db.SaveChanges();
        }
    }
}
