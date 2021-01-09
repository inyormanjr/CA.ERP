using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.StockMoveAgg
{
    public enum MoveCause : int
    {
        Unknown = 0,
        StockReceive = 1,
        Sale = 2
    }
}
