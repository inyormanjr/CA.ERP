using CA.ERP.Domain.Base;
using CA.ERP.Domain.MasterProductAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.PurchaseOrderAgg
{
    public class PurchaseOrderItem:ModelBase
    {
        public Guid PurchaseOrderId { get; set; }
        public Guid MasterProductId { get; set; }
        public decimal OrderedQuantity { get; set; }
        public decimal FreeQuantity { get; set; }
        public decimal TotalQuantity { get; set; }
        public decimal CostPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalCostPrice { get; set; }
        public decimal DeliveredQuantity { get; set; }
        public PurchaseOrderItemStatus PurchaseOrderItemStatus { get; set; }

        public string BrandName { get; set; }
        public string Model { get; set; }

        public PurchaseOrder PurchaseOrder { get; set; }
        public MasterProduct MasterProduct { get; set; }
    }
}
