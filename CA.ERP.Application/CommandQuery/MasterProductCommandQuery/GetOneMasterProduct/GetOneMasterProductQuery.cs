using CA.ERP.Domain.BranchAgg;
using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Domain.MasterProductAgg;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Application.CommandQuery.MasterProductCommandQuery.GetOneMasterProduct
{
    public class GetOneMasterProductQuery : IRequest<DomainResult<MasterProduct>>
    {
        public Guid Id { get; set; }

        public GetOneMasterProductQuery(Guid id)
        {
            Id = id;
        }
    }
}
