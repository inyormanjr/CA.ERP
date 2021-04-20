using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Common.Types
{
    public enum StockStatus : int
    {
        Unknown = 0,
        Available = 1,
        Free = 2,
        Sold = 3
    }
}
