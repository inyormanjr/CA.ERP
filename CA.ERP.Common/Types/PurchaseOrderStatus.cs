using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Common.Types
{
    public enum PurchaseOrderStatus : int
    {
        Pending = 0,
        Received = 1,
        Cancelled = 2,
        Generated = 4
    }
}
