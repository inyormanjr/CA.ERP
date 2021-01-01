using System;
using System.Collections.Generic;

namespace CA.ERP.WebApp.Dto.Supplier
{
    public class SupplierBrandView : DtoViewBase
    {
        public Guid SupplierId { get; set; }
        public Guid BrandId { get; set; }
        public string Brand { get; set; }
        public List<MasterProductLiteView> MasterProducts { get; set; } = new List<MasterProductLiteView>();

    }
}