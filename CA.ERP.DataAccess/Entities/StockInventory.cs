using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.DataAccess.Entities
{
    public class StockInventory
    {
        public Guid MasterProductId { get; set; }
        public Guid BranchId { get; set; }
        public decimal Quantity { get; set; }

        public MasterProduct MasterProduct { get; set; }
        public Branch Branch { get; set; }

        public List<StockMove> StockMoves { get; set; }
    }
}
