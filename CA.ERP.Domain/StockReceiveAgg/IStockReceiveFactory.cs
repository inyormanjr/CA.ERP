using CA.ERP.Domain.Base;
using CA.ERP.Domain.StockAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.StockReceiveAgg
{
    public interface IStockReceiveFactory: IFactory<StockReceive>
    {
        StockReceive CreateStockReceive(Guid? purchaseOrderId, Guid branchId, StockSource stockSource, Guid supplierId, List<Stock> stocks);
    }
}
