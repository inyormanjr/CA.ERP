using CA.ERP.Domain.Core.DomainResullts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Application.CommandQuery.BrandCommandQuery.DeleteBrand
{
    public class DeleteBrandCommand : IRequest<DomainResult>
    {
        public Guid Id { get; set; }

        public DeleteBrandCommand(Guid id)
        {
            Id = id;
        }
    }
}
