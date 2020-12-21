﻿using CA.ERP.Domain.Base;
using CA.ERP.Domain.UserAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.BranchAgg
{
    public class Branch : ModelBase
    {
        public string Name { get; set; }
        public int BranchNo { get; set; }
        public string Code { get; set; }
        public string Address  { get; set; }
        public string Contact { get; set; }
        public List<UserBranch> UserBranches { get; set; }
    }
}
