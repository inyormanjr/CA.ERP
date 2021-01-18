using CA.ERP.Domain.StockAgg;
using CA.ERP.Domain.StockInventoryAgg;
using CA.ERP.Domain.StockMoveAgg;
using CA.ERP.Domain.StockReceiveAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.StockMoveAgg
{
    public interface IStockReceiveStockMoveGenerator : IStockMoveGenerator
    {
        StockMove Generate(Guid stockReceiveId, StockMove previousStockMove, Stock stock);
    }
}
