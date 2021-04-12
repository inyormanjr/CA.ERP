using CA.ERP.Domain.Core.EventBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.PurchaseOrderAgg
{
    public class PurchaseOrderCreated : IDomainEvent
    {
        public PurchaseOrder PurchaseOrder { get;  }
        public PurchaseOrderCreated(PurchaseOrder purchaseOrder)
        {
            PurchaseOrder = purchaseOrder;
        }
    }
}
