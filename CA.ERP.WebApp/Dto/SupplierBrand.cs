using System;

namespace CA.ERP.WebApp.Dto
{
    public class SupplierBrand : DtoViewBase
    {
        public Guid SupplierId { get; set; }
        public Guid BrandId { get; set; }
    }
}