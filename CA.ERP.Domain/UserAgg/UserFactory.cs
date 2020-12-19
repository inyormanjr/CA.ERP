﻿using CA.ERP.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.UserAgg
{
    public class UserFactory : IUserFactory
    {
        public User CreateUser(string username, string password, int branchId)
        {
            return new User() { Username = username, BranchId = branchId };
        }

    }
}