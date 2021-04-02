using CA.ERP.Domain.Base;
using CA.ERP.Domain.BrandAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.MasterProductAgg
{
    public class MasterProduct: ModelBase
    {
        private  MasterProduct()
        {
            ProductStatus = ProductStatus.Provisional;
        }
        public string Model { get; private set; }
        public string Description { get; private set; }
        public ProductStatus ProductStatus { get; private set; }
        public Guid BrandId { get; private set; }

        public void Update(string model, string description, Guid brandId, ProductStatus productStatus)
        {
            CheckModel(model);
            CheckBrandid(brandId);

            Model = model;
            Description = description;
            BrandId = brandId;
            ProductStatus = productStatus;
        }

        

        public static MasterProduct Create(string model, string description, Guid brandId)
        {
            CheckModel(model);
            CheckBrandid(brandId);
            return new MasterProduct() { Model = model, Description = description, ProductStatus = ProductStatus.Provisional, BrandId = brandId };
        }

        private static void CheckBrandid(Guid brandId)
        {
            if (brandId == Guid.Empty)
            {
                throw new MasterProductException(MasterProductException.EmptyBrandId, "Brand Id is empty.");
            }
        }

        private static void CheckModel(string model)
        {
            if (string.IsNullOrEmpty(model))
            {
                throw new MasterProductException(MasterProductException.InvalidModel, "Product model is invalid.");
            }
        }
    }
}
