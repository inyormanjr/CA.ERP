using CA.ERP.Domain.Base;
using CA.ERP.Domain.BranchAgg;
using CA.ERP.Domain.SupplierAgg;
using CA.ERP.Domain.UserAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CA.ERP.Domain.PurchaseOrderAgg
{
    public class PurchaseOrder: ModelBase
    {
        public PurchaseOrder()
        {
            PurchaseOrderItems = new List<PurchaseOrderItem>();
        }
        public string Barcode { get; set; }
        public DateTime DeliveryDate { get; set; }
        public decimal TotalCostPrice { get; set; }
        public Guid ApprovedById { get; set; }
        public Guid SupplierId { get; set; }
        public Guid BranchId { get; set; }

        public string SupplierName { get; set; }
        public string BranchName { get; set; }
        public string BranchAddress { get; set; }
        public decimal TotalFreeQuantity
        {
            get
            {
                return PurchaseOrderItems.Select(poi => poi.FreeQuantity).Sum();
            }
        }

        public decimal TotalOrderedQuantity
        {
            get
            {
                return PurchaseOrderItems.Select(poi => poi.OrderedQuantity).Sum();
            }
        }

        public List<PurchaseOrderItem> PurchaseOrderItems { get; set; } = new List<PurchaseOrderItem>();

        public User ApprovedBy { get; set; }
        public Supplier Supplier { get; set; }
        public Branch Branch { get; set; }
    }
}
