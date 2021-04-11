using CA.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.Identity.Data
{
    public class UserBranch
    {
        public string UserId { get; set; }
        public string BranchId { get; set; }
        public string BranchName { get; set; }
        public string BranchCode { get; set; }
        public ApplicationUser User { get;  set; }
    }
}
