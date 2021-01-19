using CA.ERP.Domain.Base;
using CA.ERP.Domain.StockInventoryAgg;
using CA.ERP.Domain.StockReceiveAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.StockMoveAgg
{
    public class StockMove : ModelBase
    {
        public Guid MasterProductId { get; set; }
        public Guid BranchId { get; set; }
        public DateTime MoveDate { get; set; }
        public MoveCause MoveCause { get; set; }
        public decimal PreviousQuantity { get; set; }
        public decimal ChangeQuantity { get; set; }
        public decimal CurrentQuantity { get; set; }
        public Guid? StockReceiveId { get; set; }

        public StockInventoryAgg.StockInventory StockInventory { get; set; }

        public StockReceive StockReceive { get; set; }
    }
}
