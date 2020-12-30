using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Dto.PurchaseOrder
{
    public class PurchaseOrderItemCreate
    {
        
        [Required]
        public Guid MasterProductId { get; set; }
        public decimal OrderedQuantity { get; set; }
        public decimal FreeQuantity { get; set; }
        public decimal TotalQuantity { get; set; }
        public decimal CostPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalCostPrice { get; set; }
        public decimal DeliveredQuantity { get; set; }
    }
}
