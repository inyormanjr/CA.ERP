using System;

namespace CA.ERP.WebApp.Dto
{
    public class SupplierBrand : DtoBase
    {
        public Guid SupplierId { get; set; }
        public Guid BrandId { get; set; }
    }
}