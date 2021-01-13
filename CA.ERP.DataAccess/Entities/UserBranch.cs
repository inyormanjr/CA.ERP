using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.DataAccess.Entities
{
    public class UserBranch
    {
        public Guid UserId { get; set; }
        public Guid BranchId { get; set; }
        public User User { get; set; }
        public Branch Branch { get; set; }
    }
}
