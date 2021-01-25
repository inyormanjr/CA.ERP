using CA.ERP.Domain.UserAgg;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Dto.User
{
    public class UserCreate
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public UserRole Role { get; set; }
        [Required]
        public List<UserBranchCreate> Branches { get; set; }
    }
}
