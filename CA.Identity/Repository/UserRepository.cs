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
    }
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
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
                query = query.Where(u => u.FirstName.StartsWith(firstName));
            }
            if (!string.IsNullOrEmpty(lastName))
            {
                query = query.Where(u => u.LastName.StartsWith(lastName));
            }
            if (!string.IsNullOrEmpty(userName))
            {
                query = query.Where(u => u.UserName.StartsWith(userName));
            }


            return query;
        }
    }
}
