using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.PurchaseOrderAgg
{
    public class PurchaseOrderItemTotalQuantityCalculator : IPurchaseOrderItemTotalQuantityCalculator
    {
        public decimal Calculate(PurchaseOrderItem purchaseOrderItem)
        {
            return purchaseOrderItem.FreeQuantity + purchaseOrderItem.OrderedQuantity;
        }
    }
}
