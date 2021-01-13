using CA.ERP.Domain.Base;
using CA.ERP.Domain.BranchAgg;
using System;
using System.Collections.Generic;

namespace CA.ERP.Domain.UserAgg
{
    public class User : ModelBase
    {
        public User()
        {
            UserBranches = new List<UserBranch>();
        }
        public string Username { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRole Role { get; set; }
        public  List<UserBranch> UserBranches { get; set; }

        public void SetHashAndSalt(byte[] hash, byte[] salt)
        {
            this.PasswordHash = hash;
            this.PasswordSalt = salt;
        }

    }
}
