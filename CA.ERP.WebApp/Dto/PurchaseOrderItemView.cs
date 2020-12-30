using System;

namespace CA.ERP.WebApp.Dto
{
    public class PurchaseOrderItemView : DtoViewBase
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

    }
}