using CA.ERP.Domain.Base;
using CA.ERP.Domain.BrandAgg;
using CA.ERP.Domain.Core;
using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Domain.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.MasterProductAgg
{
    public class MasterProduct : IEntity
    {
        private MasterProduct()
        {
            ProductStatus = ProductStatus.Provisional;
        }
        public Guid Id { get; private set; }

        public Status Status { get; private set; }

        public string Model { get; private set; }
        public string Description { get; private set; }
        public ProductStatus ProductStatus { get; private set; }
        public Guid BrandId { get; private set; }

        

        public DomainResult Update(string model, string description, Guid brandId, ProductStatus productStatus)
        {
            if (string.IsNullOrEmpty(model))
            {
                return DomainResult<MasterProduct>.Error(MasterProductErrorCodes.InvalidModel, "Product model is invalid.");
            }
            if (brandId == Guid.Empty)
            {
                return DomainResult<MasterProduct>.Error(MasterProductErrorCodes.EmptyBrandId, "Brand Id is empty.");
            }

            Model = model;
            Description = description;
            BrandId = brandId;
            ProductStatus = productStatus;

            return DomainResult.Success();
        }



        public static DomainResult<MasterProduct> Create(string model, string description, Guid brandId)
        {
            if (string.IsNullOrEmpty(model))
            {
                return DomainResult<MasterProduct>.Error(MasterProductErrorCodes.InvalidModel, "Product model is invalid.");
            }
            if (brandId == Guid.Empty)
            {
                return DomainResult<MasterProduct>.Error(MasterProductErrorCodes.EmptyBrandId, "Brand Id is empty.");
            }

            var result = new MasterProduct() { Model = model, Description = description, ProductStatus = ProductStatus.Provisional, BrandId = brandId };
            return DomainResult<MasterProduct>.Success(result);
        }


    }
}
