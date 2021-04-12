using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Domain.MasterProductAgg;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Application.CommandQuery.MasterProductCommandQuery.CreateMasterProduct
{
    public class CreateMasterProductCommand : IRequest<DomainResult<Guid>>
    {

        public string Model { get; set; }
        public string Description { get; set; }
        public Guid BrandId { get; set; }
        public CreateMasterProductCommand(string model, string description, Guid brandId)
        {
            Model = model;
            Description = description;
            BrandId = brandId;
        }
    }
}
