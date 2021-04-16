using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.Reporting.Dto
{
    public class PurchaseOrder
    {
        public string Number { get; set; }

        public DateTimeOffset OrderedDate { get; set; }
        public DateTimeOffset DeliveryDate { get; set; }


        public decimal TotalCostPrice { get; set; }

        public string BranchAddress { get; set; }

        public string SupplierName { get; set; }

        public decimal TotalOrderedQuantity { get; set; }

        public decimal TotalFreeQuantity { get; set; }

        public string Barcode { get; set; }

        public List<PurchaseOrderItem> PurchaseOrderItems { get; set; }
    }
}
