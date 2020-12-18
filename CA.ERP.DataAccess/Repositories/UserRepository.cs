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
using System.Threading;

namespace CA.ERP.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CADataContext _context;
        private readonly IMapper _mapper;

        public UserRepository(CADataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Dom.User> GetUserByUsernameAsync(string username, CancellationToken cancellationToken = default)
        {
            var user = await this._context.Users.FirstOrDefaultAsync(x => x.Username == username);
            if (user == null) return null;

            return _mapper.Map<Dom.User>(user);
        }

        public async Task<Dom.User> Add(Dom.User user)
        {

            var dalUser = _mapper.Map<Dal.User>(user);
            await _context.Users.AddAsync(dalUser);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
