using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.BranchAgg
{
    public class Branch
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BranchNo { get; set; }
        public string Code { get; set; }
        public string Address  { get; set; }
        public string Contact { get; set; }
    }
}
