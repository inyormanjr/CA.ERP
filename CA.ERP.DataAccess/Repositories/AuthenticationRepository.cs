using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading.Tasks;
using CA.ERP.Domain.Helpers;
using Dom = CA.ERP.Domain.UserAgg;
using Dal = CA.ERP.DataAccess.Entities;
using AutoMapper;
using CA.ERP.Domain.UserAgg;

namespace CA.ERP.DataAccess.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly CADataContext _context;
        private readonly IMapper _mapper;

        public AuthenticationRepository(CADataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Dom.User> Login(string username, string password)
        {
            var user = await this._context.Users.FirstOrDefaultAsync(x => x.Username == username);
            if (user == null) return null;

            if (!PasswordManagementHelper.VerifyPasswordhash(password, user.PasswordHash, user.PasswordSalt)) return null;
            return _mapper.Map<Dom.User>(user);
        }

        public async Task<Dom.User> Register(Dom.User user, string password)
        {
            //should move to a user service
            byte[] passwordHash, passwordSalt;
            PasswordManagementHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            user.SetHashAndSalt(passwordHash, passwordSalt);

            var dalUser = _mapper.Map<Dal.User>(user);
            await _context.Users.AddAsync(dalUser);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
