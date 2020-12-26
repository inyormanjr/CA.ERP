using CA.ERP.Domain.Base;
using CA.ERP.Domain.BrandAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.MasterProductAgg
{
    public class MasterProduct: ModelBase
    {
        public MasterProduct()
        {
            ProductStatus = ProductStatus.Provisional;
        }
        public string Model { get; set; }
        public string Description { get; set; }
        public ProductStatus ProductStatus { get; set; }
        public Guid BrandId { get; set; }
        public Brand Brand { get; set; }
    }
}
