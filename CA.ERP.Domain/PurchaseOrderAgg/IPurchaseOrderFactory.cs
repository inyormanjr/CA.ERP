using CA.ERP.Domain.Base;
using System;
using System.Collections.Generic;

namespace CA.ERP.Domain.PurchaseOrderAgg
{
    public interface IPurchaseOrderFactory : IFactory<PurchaseOrder>
    {
        PurchaseOrder Create(DateTime deliveryDate, Guid supplierId, Guid branchId, List<PurchaseOrderItem> purchaseOrderItems);
    }
}