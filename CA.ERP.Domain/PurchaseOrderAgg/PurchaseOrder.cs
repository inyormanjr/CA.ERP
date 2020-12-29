using CA.ERP.Domain.Base;
using CA.ERP.Domain.BranchAgg;
using CA.ERP.Domain.SupplierAgg;
using CA.ERP.Domain.UserAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.PurchaseOrderAgg
{
    public class PurchaseOrder: ModelBase
    {

        public string Barcode { get; set; }
        public DateTime DeliveryDate { get; set; }
        public decimal TotalAmount { get; set; }
        public Guid ApprovedById { get; set; }
        public Guid SupplierId { get; set; }
        public Guid BranchId { get; set; }

        public User ApprovedBy { get; set; }
        public Supplier Supplier { get; set; }
        public Branch Branch { get; set; }
    }
}
