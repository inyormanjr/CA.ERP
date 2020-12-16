using CA.ERP.Lib.DAL.IRepositories;
using CA.ERP.Lib.Domain.UserAgg;
using CA.ERP.Lib.Helpers;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.Lib.DAL.Repositories
{
    public class AuthRepo : IAuthRepo
    {
        private readonly CADataContext context;

        public AuthRepo(CADataContext context)
        {
            this.context = context;
        }
        public async Task<User> Login(string username, string password)
        {
            User user = await this.context.Users.FirstOrDefaultAsync(x => x.Username == username);
            if (user == null) return null;

            if (!PasswordManagementHelper.VerifyPasswordhash(password, user.PasswordHash, user.PasswordSalt)) return null;
            return user;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            PasswordManagementHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            user.SetHashAndSalt(passwordHash, passwordSalt);
            await this.context.Users.AddAsync(user);
            await this.context.SaveChangesAsync();
            return user;
        }
    }
}
