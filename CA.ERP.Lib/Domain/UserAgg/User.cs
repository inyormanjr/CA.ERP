using CA.ERP.Lib.Domain.BranchAgg;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Lib.Domain.UserAgg
{
    public class User
    {
        public int Id { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRole Role { get; set; }
        public int BranchId { get; set; }
        public virtual Branch Branch { get; set; }
    }
}
