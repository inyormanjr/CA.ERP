using CA.ERP.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.DataAccess.Entities
{
    public class StockReceiveItem : EntityBase
    {
        public Guid MasterProductId { get; private set; }
        public Guid StockReceiveId { get; private set; }
        public Guid? PurchaseOrderItemId { get; private set; }
        public Guid BranchId { get; private set; }
        public string StockNumber { get; private set; }
        public string SerialNumber { get; private set; }
        public StockStatus StockStatus { get; private set; }
        public decimal CostPrice { get; private set; }
        public StockReceive StockReceive { get; set; }
        public MasterProduct MasterProduct { get; set; }
        public PurchaseOrderItem PurchaseOrderItem { get; set; }
        public Branch Branch { get; set; }

    }
}
