using CA.ERP.Common.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Shared.Dto.StockReceive
{
    public class StockReceiveItemCommit
    {
        public Guid Id { get; set; }
        public Guid? PurchaseOrderItemId { get;  set; }
        public string StockNumber { get; set; }
        public StockStatus StockStatus { get; set; }
        public string SerialNumber { get; set; }
        public decimal CostPrice { get; set; }
    }
}
