using CA.ERP.Domain.StockAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.DataAccess.Entities
{
    public class Stock: EntityBase
    {
        public Guid MasterProductId { get; set; }
        public Guid StockReceiveId { get; set; }
        public Guid? PurchaseOrderItemId { get; set; }
        public string StockNumber { get; set; }
        public string SerialNumber { get; set; }
        public StockStatus StockStatus { get; set; }
        public decimal CostPrice { get; set; }

        public MasterProduct MasterProduct { get; set; }
        public StockReceive StockReceive { get; set; }
        public PurchaseOrderItem PurchaseOrderItem { get; set; }
    }
}
