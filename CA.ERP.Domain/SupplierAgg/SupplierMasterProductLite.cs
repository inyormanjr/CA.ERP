using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.SupplierAgg
{
    public class SupplierMasterProductLite
    {
        public Guid SupplierId { get; set; }
        public Guid MasterProductId { get; set; }
        public string Model { get; set; }
        public decimal CostPrice { get; set; }
    }
}
