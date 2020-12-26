using CA.ERP.Domain.Base;
using System;

namespace CA.ERP.Domain.MasterProductAgg
{
    public interface IMasterProductFactory: IFactory<MasterProduct>
    {
        MasterProduct CreateMasterProduct(string model, string description, ProductStatus productStatus, Guid brandId);
    }
}