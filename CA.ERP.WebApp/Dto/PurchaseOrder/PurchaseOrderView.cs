using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Dto.PurchaseOrder
{
    public class PurchaseOrderView : DtoViewBase
    {
        public string Barcode { get; set; }
        public DateTime DeliveryDate { get; set; }
        public decimal TotalCostPrice { get; set; }
        public Guid ApprovedById { get; set; }
        public Guid SupplierId { get; set; }
        public Guid BranchId { get; set; }

        public List<PurchaseOrderItemView> PurchaseOrderItems { get; set; }
    }
}
