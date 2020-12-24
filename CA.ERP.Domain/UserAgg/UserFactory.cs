using CA.ERP.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.UserAgg
{
    public class UserFactory : IUserFactory
    {
        public User CreateUser(string username, UserRole role, string firstName, string lastName)
        {
            return new User() { Username = username, Role = role, FirstName = firstName, LastName = lastName };
        }

    }
}
