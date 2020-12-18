using CA.ERP.Domain.Base;
using CA.ERP.Lib.Domain.UserAgg;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.Domain.UserAgg
{
    public interface IAuthenticationRepository : IRepository
    {
        Task<User> Register(User user, string password);

        Task<User> Login(string username, string password);
        
    }
}
