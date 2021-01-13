using CA.ERP.Domain.StockAgg;
using CA.ERP.Domain.StockMoveAgg;
using CA.ERP.Domain.StockReceiveAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CA.ERP.Domain.StockInventoryAgg
{
    public class StockInventoryStockReceiveCalculator : IStockInventoryStockReceiveCalculator
    {
        public StockInventory CalculateStockInventory(StockInventory stockInventory, Guid stockReceiveId, StockMove previousStockMove, Stock stock)
        {
            var stockMove = new StockMove();
            stockMove.MasterProductId = stock.MasterProductId;
            stockMove.BranchId = stock.BranchId;
            stockMove.MoveDate = DateTime.Now;
            stockMove.MoveCause = MoveCause.StockReceive;
            stockMove.PreviousQuantity = previousStockMove?.CurrentQuantity ?? 0;
            stockMove.ChangeQuantity = 1;
            stockMove.CurrentQuantity = stockMove.PreviousQuantity + stockMove.ChangeQuantity;
            stockMove.StockReceiveId = stockReceiveId;

            stockInventory.Quantity += stockMove.ChangeQuantity;
            stockInventory.StockMoves.Add(stockMove);

            return stockInventory;
        }
    }
}
