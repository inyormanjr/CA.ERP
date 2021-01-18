using CA.ERP.Domain.StockAgg;
using CA.ERP.Domain.StockInventoryAgg;
using CA.ERP.Domain.StockMoveAgg;
using CA.ERP.Domain.StockReceiveAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CA.ERP.Domain.StockMoveAgg
{
    public class StockInventoryStockReceiveCalculator : IStockReceiveStockMoveGenerator
    {

        public StockMove Generate(Guid stockReceiveId, StockMove previousStockMove, Stock stock)
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
            return stockMove;
        }
    }
}
