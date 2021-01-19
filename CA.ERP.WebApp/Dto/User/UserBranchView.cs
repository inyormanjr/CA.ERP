using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Dto.User
{
    public class UserBranchView
    {
        public Guid UserId { get; set; }
        public Guid BranchId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        
    }
}
