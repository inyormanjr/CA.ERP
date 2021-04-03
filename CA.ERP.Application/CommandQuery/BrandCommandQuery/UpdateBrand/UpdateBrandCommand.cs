using CA.ERP.Domain.Core.DomainResullts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Application.CommandQuery.BrandCommandQuery.UpdateBrand
{
    public class UpdateBrandCommand : IRequest<DomainResult>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public UpdateBrandCommand(Guid id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

    }
}
