using CA.ERP.Domain.Base;
using CA.ERP.Domain.MasterProductAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.SupplierAgg
{
    public class SupplierBrand : ModelBase
    {
        public Guid SupplierId { get; set; }
        public Guid BrandId { get; set; }
        public Supplier Supplier { get; set; }
        public Brand Brand { get; set; }
    }
}
