
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
using System.Diagnostics.CodeAnalysis;
using OneOf;
using OneOf.Types;
using CA.ERP.Common.Extensions;
using CA.ERP.Domain.Base;
using CA.ERP.Domain.Common;

namespace CA.ERP.DataAccess.Repositories
{
    public class UserRepository :AbstractRepository<User, Dal.User>, IUserRepository
    {

        public UserRepository(CADataContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public async Task<OneOf<Dom.User, None>> GetUserByUsernameAsync(string username, CancellationToken cancellationToken = default)
        {
            OneOf<Dom.User, None> result = default(None);
            var user = await this._context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Status == Common.Status.Active);
            if (user != null) {
                result = _mapper.Map<Dom.User>(user);
            };

            return result;
        }

        /// <summary>
        /// Updates user except for PasswordHash and PasswordSalt
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task<OneOf<Guid, None>> UpdateAsync(Guid id, User user, CancellationToken cancellationToken = default)
        {
            OneOf<Guid, None> result = default(None);
            var dalUser = await _context.Users.Include(u=>u.UserBranches).FirstOrDefaultAsync(b => b.Id == id, cancellationToken: cancellationToken);
            if (dalUser != null)
            {
                //mark all userbranches as deleted
                foreach (var userBranch in dalUser.UserBranches)
                {
                    _context.Entry(userBranch).State = EntityState.Deleted;
                }
                _mapper.Map(user, dalUser);
                dalUser.Id = id;

                //add new user branch
                foreach (var userBranch in dalUser.UserBranches)
                {
                    _context.Entry(userBranch).State = EntityState.Added;
                }

                _context.Entry(dalUser).State = EntityState.Modified;
                _context.Entry(dalUser).Property(u => u.PasswordHash).IsModified = false;
                _context.Entry(dalUser).Property(u => u.PasswordSalt).IsModified = false;
                await _context.SaveChangesAsync(cancellationToken: cancellationToken);
                result = dalUser.Id;
            }

            return result;
        }

        public async Task<OneOf<Success, None>> UpdatePasswordAsync(Guid userId, byte[] passwordHash, byte[] passwordSalt)
        {
            OneOf<Success, None> ret = default(None);
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user != null)
            {
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                ret = default(Success);
            }
            return ret;
        }
    }
}
