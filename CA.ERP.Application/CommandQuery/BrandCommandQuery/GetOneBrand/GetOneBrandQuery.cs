using CA.ERP.Domain.BrandAgg;
using CA.ERP.Domain.Core.DomainResullts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Application.CommandQuery.BrandCommandQuery.GetOneBrand
{
    public class GetOneBrandQuery : IRequest<DomainResult<Brand>>
    {
        public Guid Id { get; set; }

        public GetOneBrandQuery(Guid id)
        {
            Id = id;
        }
    }
}
