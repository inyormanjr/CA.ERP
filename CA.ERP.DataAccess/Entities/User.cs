using CA.ERP.Domain.UserAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.DataAccess.Entities
{
    public class User : EntityBase
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
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
        public List<UserBranch> UserBranches { get; set; }

        public void SetHashAndSalt(byte[] hash, byte[] salt)
        {
            this.PasswordHash = hash;
            this.PasswordSalt = salt;
        }
    }
}
