using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Common.Types
{
    [Flags]
    public enum StockSource : int
    {
        Unknown = 0,
        Direct = 1,
        PurchaseOrder = 2,
        StockTransfer = 3,
    }
}
