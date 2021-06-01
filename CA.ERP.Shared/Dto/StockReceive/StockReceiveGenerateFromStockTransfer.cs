using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Shared.Dto.StockReceive
{
    public class StockReceiveGenerateFromStockTransfer
    {
        public Guid StockTransferId { get; set; }

        public StockReceiveGenerateFromStockTransfer(Guid stockTransferId)
        {
            StockTransferId = stockTransferId;
        }
    }
}
