using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.Shared.Dto.PurchaseOrder
{
    public class PurchaseOrderItemCreate
    {
        
        [Required]
        public Guid MasterProductId { get; set; }
        public string BrandName { get; set; }
        public string Model { get; set; }
        public decimal OrderedQuantity { get; set; }
        public decimal FreeQuantity { get; set; }
        public decimal TotalQuantity
        {
            get
            {
                return OrderedQuantity + FreeQuantity;
            }
        }
        public decimal CostPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalCostPrice
        {
            get
            {
                return OrderedQuantity * (CostPrice - Discount);
            }
        }
        public decimal DeliveredQuantity { get; set; }
    }
}
