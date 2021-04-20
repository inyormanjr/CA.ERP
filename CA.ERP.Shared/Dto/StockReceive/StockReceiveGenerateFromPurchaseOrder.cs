using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Shared.Dto.StockReceive
{
    public class StockReceiveGenerateFromPurchaseOrder
    {
        public Guid PurchaseOrderId { get; set; }
        public StockReceiveGenerateFromPurchaseOrder(Guid purchaseOrderId)
        {
            PurchaseOrderId = purchaseOrderId;
        }
        public StockReceiveGenerateFromPurchaseOrder()
        {

        }
    }
}
