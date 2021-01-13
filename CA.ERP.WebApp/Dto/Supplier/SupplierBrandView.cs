using System;
using System.Collections.Generic;

namespace CA.ERP.WebApp.Dto.Supplier
{
    public class SupplierBrandView
    {
        public Guid SupplierId { get; set; }
        public Guid BrandId { get; set; }
        public string BrandName { get; set; }
        public List<MasterProductLiteView> MasterProducts { get; set; } = new List<MasterProductLiteView>();

    }
}