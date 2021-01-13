using CA.ERP.Domain.Base;
using CA.ERP.Domain.BranchAgg;
using CA.ERP.Domain.MasterProductAgg;
using CA.ERP.Domain.StockMoveAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.StockInventoryAgg
{
    public class StockInventory:ModelBase
    {
        public Guid MasterProductId { get; set; }
        public Guid BranchId { get; set; }
        public decimal Quantity { get; set; }

        public MasterProduct MasterProduct { get; set; }
        public Branch Branch { get; set; }

        public List<StockMove> StockMoves { get; set; } = new List<StockMove>();
    }
}
