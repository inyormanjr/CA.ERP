using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.Shared.Dto.User
{
    public class UserCreate
    {

        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Password and Confirm Password dot not match.")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [Required]
        public HashSet<UserBranchCreate> Branches { get; set; } = new HashSet<UserBranchCreate>();

        [Required]
        public HashSet<string> Roles { get; set; } = new HashSet<string>();
    }
}
