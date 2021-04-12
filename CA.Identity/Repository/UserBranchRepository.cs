using CA.Identity.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.Identity.Repository
{
    public interface IUserBranchRepository
    {
        Task<List<UserBranch>> GetUserBranches(string userId);
    }
    public class UserBranchRepository : IUserBranchRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UserBranchRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public Task<List<UserBranch>> GetUserBranches(string userId)
        {
            return _applicationDbContext.UserBranches.Where(ub => ub.UserId == userId).ToListAsync();
        }
    }

    
}
