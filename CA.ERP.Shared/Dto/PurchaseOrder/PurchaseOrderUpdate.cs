using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Dto.PurchaseOrder
{
    public class PurchaseOrderUpdate
    {
        public PurchaseOrderUpdate()
        {
            PurchaseOrderItems = new List<PurchaseOrderItemUpdate>();
        }
        [Required]
        public DateTime DeliveryDate { get; set; }
        [Required]
        public Guid SupplierId { get; set; }
        [Required]
        public Guid DestinationBranchId { get; set; }

        public List<PurchaseOrderItemUpdate> PurchaseOrderItems { get; set; }
    }
}
