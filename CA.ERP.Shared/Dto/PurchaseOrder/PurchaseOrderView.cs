using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.Shared.Dto.PurchaseOrder
{
    public class PurchaseOrderView : DtoViewBase
    {
        public string Barcode { get; set; }
        public DateTimeOffset DeliveryDate { get; set; }
        public decimal TotalCostPrice { get; set; }
        public Guid ApprovedById { get; set; }
        public Guid SupplierId { get; set; }
        public Guid BranchId { get; set; }
        public string SupplierName { get; set; }
        public string BranchName { get; set; }

        public PurchaseOrderView()
        {

        }


        //public List<PurchaseOrderItemView> PurchaseOrderItems { get; set; }
    }
}
