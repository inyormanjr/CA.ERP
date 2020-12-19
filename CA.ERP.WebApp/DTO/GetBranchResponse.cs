using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Dto
{
    public class GetBranchResponse
    {
        public ICollection<Branch> Branches { get; set; }
    }
}
