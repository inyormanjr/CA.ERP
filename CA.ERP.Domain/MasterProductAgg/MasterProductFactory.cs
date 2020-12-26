using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.MasterProductAgg
{
    public class MasterProductFactory : IMasterProductFactory
    {
        public MasterProduct CreateMasterProduct(string model, string description, ProductStatus productStatus, Guid brandId)
        {
            return new MasterProduct() { Model = model, Description = description, ProductStatus = productStatus, BrandId = brandId };
        }
    }
}
