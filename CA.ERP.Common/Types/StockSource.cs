using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Common.Types
{
    [Flags]
    public enum StockSource
    {
        Unknown = 0,
        Direct = 1,
        PurchaseOrder = 2,
    }
}
