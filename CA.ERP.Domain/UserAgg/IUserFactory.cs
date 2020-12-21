using CA.ERP.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.UserAgg
{
    public interface IUserFactory : IFactory<User>
    {
        User CreateUser(string username);
    }
}
