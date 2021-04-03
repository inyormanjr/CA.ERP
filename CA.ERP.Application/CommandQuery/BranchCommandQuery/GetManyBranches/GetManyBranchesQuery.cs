using CA.ERP.Domain.BranchAgg;
using CA.ERP.Domain.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Application.CommandQuery.BranchCommandQuery.GetManyBranches
{
    public class GetManyBranchesQuery : IRequest<PaginatedList<Branch>>
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public Status Status { get; set; }

        public GetManyBranchesQuery(int skip = 0, int take = 0, Status status = Status.Active)
        {
            Skip = skip;
            Take = take;
            Status = status;
        }
    }
}
