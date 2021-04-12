using CA.ERP.Domain.BranchAgg;
using CA.ERP.Domain.Core.DomainResullts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Application.CommandQuery.BranchCommandQuery.GetOneBranch
{
    public class GetOneBranchQuery : IRequest<DomainResult<Branch>>
    {
        public Guid Id { get; set; }

        public GetOneBranchQuery(Guid id)
        {
            Id = id;
        }
    }
}
