using CA.ERP.Domain.StockReceiveAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.DataAccess.Entities
{
    public class StockReceive : EntityBase
    {
        public Guid? PurchaseOrderId { get; set; }
        public Guid BranchId { get; set; }
        public StockSource StockSouce { get; set; }

        public PurchaseOrder PurchaseOrder { get; set; }
        public Branch Branch { get; set; }

        public List<Stock> Stocks { get; set; }
    }
}
