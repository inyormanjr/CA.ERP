using CA.Identity.Data;
using CA.Identity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CA.Identity.Repository
{
    public interface IUserRepository
    {
        Task<List<ApplicationUser>> GetUsers(string firstName, string lastName, string userName, int skip = 0, int take = 100, CancellationToken cancellationToken = default);
        Task<int> GetUsersCount(string firstName, string lastName, string userName, CancellationToken cancellationToken = default);
        Task<ApplicationUser> GetUserById(string id, CancellationToken cancellationToken = default);
    }
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public Task<ApplicationUser> GetUserById(string id, CancellationToken cancellationToken = default)
        {
            return _applicationDbContext.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        }

        public Task<List<ApplicationUser>> GetUsers(string firstName, string lastName, string userName, int skip = 0, int take = 100, CancellationToken cancellationToken = default)
        {
            IQueryable<ApplicationUser> query = generateQuery(firstName, lastName, userName);
            return query.OrderBy(u => u.UserName).Skip(skip).Take(take).ToListAsync(cancellationToken);
        }

        public Task<int> GetUsersCount(string firstName, string lastName, string userName, CancellationToken cancellationToken = default)
        {
            IQueryable<ApplicationUser> query = generateQuery(firstName, lastName, userName);
            return query.OrderBy(u => u.UserName).CountAsync();
        }


        private IQueryable<ApplicationUser> generateQuery(string firstName, string lastName, string userName)
        {
            var query = _applicationDbContext.Users.Include(u=>u.UserBranches).AsQueryable();
            if (!string.IsNullOrEmpty(firstName))
            {
                query = query.Where(u => u.FirstName.ToLower().StartsWith(firstName.ToLower()));
            }
            if (!string.IsNullOrEmpty(lastName))
            {
                query = query.Where(u => u.LastName.ToLower().StartsWith(lastName.ToLower()));
            }
            if (!string.IsNullOrEmpty(userName))
            {
                query = query.Where(u => u.UserName.ToLower().StartsWith(userName.ToLower()));
            }


            return query;
        }
    }
}
