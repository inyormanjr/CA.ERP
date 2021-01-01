using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.SupplierAgg
{
    public class SupplierBrandLite
    {
        public Guid SupplierId { get; set; }
        public Guid BrandId { get; set; }
        public string BrandName { get; set; }
        public IEnumerable<SupplierMasterProductLite> MasterProducts { get; set; } = new List<SupplierMasterProductLite>();
    }
}
