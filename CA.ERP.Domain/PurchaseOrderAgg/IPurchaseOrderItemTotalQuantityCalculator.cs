using CA.ERP.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.PurchaseOrderAgg
{
    public interface IPurchaseOrderItemTotalQuantityCalculator : IBusinessLogic
    {
        decimal Calculate(PurchaseOrderItem purchaseOrderItem);
    }
}
