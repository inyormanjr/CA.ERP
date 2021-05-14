using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Common.Types
{
    public enum StockReceiveStage : int
    {
        Pending = 0,
        Commited = 1,
        Cancelled = 2,
    }
}
