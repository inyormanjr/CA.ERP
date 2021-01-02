
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.DataAccess.Entities
{
    public class Branch : EntityBase
    {
        public string Name { get; set; }
        public int BranchNo { get; set; }
        public string Code { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public List<UserBranch> UserBranches { get;  set; }
        public List<PurchaseOrder> PurchaseOrders { get; set; }
    }
}
