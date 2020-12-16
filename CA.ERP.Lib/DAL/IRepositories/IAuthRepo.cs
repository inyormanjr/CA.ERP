using CA.ERP.Lib.Domain.UserAgg;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.Lib.DAL.IRepositories
{
    public interface IAuthRepo
    {
        Task<User> Register(User user, string password);

        Task<User> Login(string username, string password);
        
    }
}
