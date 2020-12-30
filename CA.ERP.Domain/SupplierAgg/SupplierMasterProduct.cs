using CA.ERP.Domain.Base;
using CA.ERP.Domain.MasterProductAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.SupplierAgg
{
    public class SupplierMasterProduct : ModelBase
    {
        public Guid MasterProductId { get; set; }
        public Guid SupplierId { get; set; }
        public decimal CostPrice { get; set; }

        public MasterProduct MasterProduct { get; set; }
        public Supplier Supplier { get; set; }
    }
}
