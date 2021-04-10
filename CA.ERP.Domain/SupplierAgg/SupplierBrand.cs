using CA.ERP.Domain.Base;
using CA.ERP.Domain.BrandAgg;
using CA.ERP.Domain.Core.Entity;
using CA.ERP.Domain.MasterProductAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.SupplierAgg
{
    public class SupplierBrand : IValueObject
    {
        public Guid SupplierId { get; set; }
        public Guid BrandId { get; set; }
        public string BrandName { get; set; }

        protected SupplierBrand(Guid supplierId, Guid brandId, string brandName)
        {
            SupplierId = supplierId;
            BrandId = brandId;
            BrandName = brandName;
        }

        public static SupplierBrand Create(Guid supplierId, Guid brandId, string brandName)
        {
            return new SupplierBrand(supplierId, brandId, brandName);
        }
    }
}
