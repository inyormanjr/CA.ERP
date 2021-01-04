
using CA.ERP.RdlcDesign.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.PurchaseOrderAgg
{
    public class PurchaseOrderItem:ModelBase
    {
        public Guid PurchaseOrderId { get; set; }
        public Guid MasterProductId { get; set; }
        public string BrandName { get; set; }
        public string Model { get; set; }
        public decimal OrderedQuantity { get; set; }
        public decimal FreeQuantity { get; set; }
        public decimal TotalQuantity { get; set; }
        public decimal CostPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalCostPrice { get; set; }
        public decimal DeliveredQuantity { get; set; }
        public int PurchaseOrderItemStatus { get; set; }

    }
}
