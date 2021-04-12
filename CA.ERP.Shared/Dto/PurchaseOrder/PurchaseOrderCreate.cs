using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.Shared.Dto.PurchaseOrder
{
    public class PurchaseOrderCreate
    {
        public PurchaseOrderCreate()
        {
            PurchaseOrderItems = new List<PurchaseOrderItemCreate>();
        }
        [Required]
        public DateTimeOffset DeliveryDate { get; set; }
        [Required]
        public Guid SupplierId { get; set; }
        [Required]
        public Guid DestinationBranchId { get; set; }

        public List<PurchaseOrderItemCreate> PurchaseOrderItems { get; set; }
    }
}
