using CA.ERP.DataAccess;
using CA.ERP.Utilities.PrevDataModel;
using CA.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using New = CA.ERP.DataAccess.Entities;


namespace CA.ERP.Utilities
{
    public class UserMigrator
    {
        private readonly CitiAppDatabaseContext _citiAppDatabaseContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserMigrator(CitiAppDatabaseContext citiAppDatabaseContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _citiAppDatabaseContext = citiAppDatabaseContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<List<ApplicationUser>> Migrate( List<New.Branch> newBranches, CancellationToken cancellationToken = default)
        {
            Console.WriteLine("Migrating users");
            //generate users
            var oldUsers = _citiAppDatabaseContext.Users.ToList();
            var newUsers = new List<ApplicationUser>();
            foreach (var oldUser in oldUsers)
            {
                ApplicationUser newUser = new ApplicationUser();
                newUser.UserName = String.Concat( oldUser.Username.Where(c => char.IsDigit(c) || char.IsLetter(c)));


                var role = oldUser.Role.Replace("&", "");

                newUser.FirstName = oldUser.FName;
                newUser.LastName = oldUser.LName;

                


                var newBranch = newBranches.FirstOrDefault(b => b.BranchNo == Migrator.BranchNumberComverter(oldUser.BranchNo));
                if (newBranch != null)
                {
                    newUser.UserBranches.Add(new Identity.Data.UserBranch() { UserId = newUser.Id, BranchId = newBranch.Id.ToString() });
                }

                

                var result = await _userManager.CreateAsync(newUser, oldUser.Password);
                if (!result.Succeeded)
                {
                    Console.WriteLine(string.Join(',', result.Errors.Select(e => e.Code)));
                    throw new Exception("Identity error");
                }
               
                try
                {
                    if (!await _roleManager.RoleExistsAsync(role))
                    {
                        var identityRole = new IdentityRole(role);
                        await _roleManager.CreateAsync(identityRole);
                    }
                    await _userManager.AddToRoleAsync(newUser, role);
                }
                catch (Exception)
                {

                    throw;
                }
                //role
               
                
                newUsers.Add(newUser);


            }

            return newUsers;
        }
    }
}
