using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.PurchaseOrderAgg
{
    public class PurchaseOrderFactory : IPurchaseOrderFactory
    {
        public PurchaseOrder Create(string barcode, DateTime deliveryDate, Guid approvedById, Guid supplierId, Guid branchId)
        {
            return new PurchaseOrder() { Barcode = barcode, DeliveryDate = deliveryDate, ApprovedById = approvedById, SupplierId = supplierId, BranchId = branchId };
        }
    }
}
