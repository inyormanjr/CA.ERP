using CA.ERP.Domain.Base;
using OneOf;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Domain.UserAgg
{
    public interface IUserRepository : IRepository<User>
    {

        Task<OneOf<User, None>> GetUserByUsernameAsync(string username, CancellationToken cancellationToken);
        
    }
}
