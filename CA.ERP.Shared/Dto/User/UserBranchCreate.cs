using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.Shared.Dto.User
{
    public class UserBranchCreate
    {
        public string BranchId { get; set; }
        public string Name { get; set; }


        public static UserBranchCreate Create(string branchId, string name)
        {
            return new UserBranchCreate() { BranchId = branchId, Name = name };
        }
    }
}
