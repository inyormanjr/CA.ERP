using CA.ERP.Domain.StockAgg;
using CA.ERP.Domain.StockMoveAgg;
using CA.ERP.Domain.StockReceiveAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.StockInventoryAgg
{
    public interface IStockInventoryStockReceiveCalculator : IStockInventoryCalculator
    {
        StockInventory CalculateStockInventory(StockInventory stockInventory, Guid stockReceiveId, StockMove previousStockMove, Stock stock);
    }
}
