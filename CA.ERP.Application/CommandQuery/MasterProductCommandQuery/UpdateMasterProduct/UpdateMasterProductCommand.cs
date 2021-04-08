using CA.ERP.Common.Types;
using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Domain.MasterProductAgg;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Application.CommandQuery.MasterProductCommandQuery.UpdateMasterProduct
{
    public class UpdateMasterProductCommand : IRequest<DomainResult>
    {
        public Guid Id { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public Guid BrandId { get; set; }
        public ProductStatus ProductStatus { get; set; }

        public UpdateMasterProductCommand(Guid id, string model, string description, Guid brandId, ProductStatus productStatus)
        {
            Id = id;
            Model = model;
            Description = description;
            BrandId = brandId;
            ProductStatus = productStatus;
        }
    }
}
