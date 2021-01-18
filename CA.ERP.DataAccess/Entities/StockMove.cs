using CA.ERP.Domain.StockMoveAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.DataAccess.Entities
{
    public class StockMove : EntityBase
    {
        public Guid MasterProductId { get; set; }
        public Guid BranchId { get; set; }
        public DateTime MoveDate { get; set; }
        public MoveCause MoveCause { get; set; }
        public decimal PreviousQuantity { get; set; }
        public decimal ChangeQuantity { get; set; }
        public decimal CurrentQuantity { get; set; }
        public Guid? StockReceiveId { get; set; }

        public StockInventory StockInventory { get; set; }

        public StockReceive StockReceive { get; set; }
    }
}
