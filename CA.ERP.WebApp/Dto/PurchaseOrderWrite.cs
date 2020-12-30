using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Dto
{
    public class PurchaseOrderWrite
    {
        public PurchaseOrderWrite()
        {
            PurchaseOrderItems = new List<PurchaseOrderItemWrite>();
        }
        [Required]
        public DateTime DeliveryDate { get; set; }
        [Required]
        public Guid SupplierId { get; set; }
        [Required]
        public Guid BranchId { get; set; }

        public List<PurchaseOrderItemWrite> PurchaseOrderItems { get; set; }
    }
}
