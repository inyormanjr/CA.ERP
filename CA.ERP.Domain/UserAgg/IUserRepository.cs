using CA.ERP.Domain.Base;
using CA.ERP.Lib.Domain.UserAgg;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Domain.UserAgg
{
    public interface IUserRepository : IRepository
    {
        Task<User> Add(User user);

        Task<User> GetUserByUsernameAsync(string username, CancellationToken cancellationToken);
        
    }
}
