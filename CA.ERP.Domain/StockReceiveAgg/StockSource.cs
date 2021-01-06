using System;

namespace CA.ERP.Domain.StockReceiveAgg
{
    [Flags]
    public enum StockSource
    {
        Unknown = 0,
        Direct = 1,
        PurchaseOrder = 2,
    }
}