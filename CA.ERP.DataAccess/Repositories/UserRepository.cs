
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
using System.Linq;

namespace CA.ERP.DataAccess.Repositories
{
    public class UserRepository :AbstractRepository<User, Dal.User>, IUserRepository
    {

        public UserRepository(CADataContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public async Task<int> CountAsync(string username, string firstName, string lastName, UserRole userRole, CancellationToken cancellationToken)
        {
            var query = _context.Users.AsQueryable();
            query = prepareQuery(username, firstName, lastName, userRole, query);

            return await query.CountAsync(cancellationToken);

        }

        private  IQueryable<Dal.User> prepareQuery(string username, string firstName, string lastName, UserRole userRole, IQueryable<Dal.User> query)
        {
            if (!string.IsNullOrEmpty(username))
            {
                query = query.Where(u => u.Username.StartsWith(username));
            }

            if (!string.IsNullOrEmpty(username))
            {
                query = query.Where(u => u.Username.StartsWith(username));
            }

            if (!string.IsNullOrEmpty(firstName))
            {
                query = query.Where(u => u.FirstName.StartsWith(firstName));
            }

            if (!string.IsNullOrEmpty(lastName))
            {
                query = query.Where(u => u.LastName.StartsWith(lastName));
            }
            if (userRole != UserRole.All)
            {
                query = query.Where(u => userRole.HasFlag(u.Role));
            }

            return query;
        }

        public async Task<List<User>> GetManyAsync(string username, string firstName, string lastName, UserRole userRole, CancellationToken cancellationToken, int skip, int take)
        {
            var query = _context.Users.AsQueryable();
            query = prepareQuery(username, firstName, lastName, userRole, query);

            return await query.OrderBy(u => u.Username).Skip(skip).Take(take).Select(u => _mapper.Map<User>(u)).ToListAsync(cancellationToken);
        }

        public async Task<OneOf<Dom.User, None>> GetUserByUsernameAsync(string username, CancellationToken cancellationToken = default)
        {
            OneOf<Dom.User, None> result = default(None);
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Status == Status.Active);
            if (user != null) {
                result = _mapper.Map<Dom.User>(user);
            };

            return result;
        }

        public async Task<OneOf<User, None>> GetUserWithBranchesAsync(Guid userId, CancellationToken cancellationToken)
        {
            OneOf<User, None> ret = default(None);
            var user = await _context.Users.Include(u => u.UserBranches).ThenInclude(ub=>ub.Branch).FirstOrDefaultAsync(u => u.Id == userId, cancellationToken: cancellationToken);
            if (user != null)
            {
                ret = _mapper.Map<User>(user);
            }
            return ret;
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
                ret = default(Success);
            }
            return ret;
        }

        public async Task UpdateUserRefreshTokenAsync(Guid id, string refreshToken, DateTime expiration, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiration = expiration;
                _context.Entry(user).State = EntityState.Modified;
            }
        }

        public async Task<OneOf<User, None>> GetUserByByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
        {
            OneOf<Dom.User, None> result = default(None);

            var user = await _context.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken && u.RefreshTokenExpiration >= DateTime.Now && u.Status == Status.Active, cancellationToken);
            if (user != null)
            {
                result = _mapper.Map<Dom.User>(user);
            };

            return result;
        }
    }
}
