using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.RdlcDesign.Models
{
    public class PurchaseOrder : ModelBase
    {
        public string Barcode { get; set; }
        public DateTime DeliveryDate { get; set; }
        public decimal TotalCostPrice { get; set; }
        public Guid ApprovedById { get; set; }
        public Guid SupplierId { get; set; }
        public Guid BranchId { get; set; }

        public string SupplierName { get; set; }
        public string BranchName { get; set; }
        public string BranchAddress { get; set; }
    }
}
