using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.Shared.Dto.User
{
    public class UserUpdate
    {
        [Required]
        [MinLength(3)]
        public string UserName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [Required]
        public List<UserBranchCreate> Branches { get; set; } = new List<UserBranchCreate>();

        [Required]
        [MinLength(1)]
        public HashSet<string> Roles { get; set; } = new HashSet<string>();
    }
}
