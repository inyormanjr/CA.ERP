using CA.ERP.Common.Types;
using CA.ERP.Shared.Dto.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.Shared.Dto.StockReceive
{
    public class StockReceiveCreate 
    {
        public Guid? PurchaseOrderId { get; set; }
        public Guid BranchId { get; set; }
        public StockSource StockSource { get; set; }
        public Guid SupplierId { get; set; }

        public List<StockCreate> Stocks { get; set; } = new List<StockCreate>();
        public string DeliveryReference { get; set; }
    }
}
