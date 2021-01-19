using CA.ERP.Domain.BranchAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.UserAgg
{
    public class UserBranch
    {
        public Guid UserId { get; set; }
        public Guid BranchId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public User User { get; set; }
        public Branch Branch { get; set; }
    }
}
