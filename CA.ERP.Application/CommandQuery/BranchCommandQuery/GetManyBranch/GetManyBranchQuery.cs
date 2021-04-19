using CA.ERP.Common.Types;
using CA.ERP.Domain.BranchAgg;
using CA.ERP.Domain.Core;
using CA.ERP.Shared.Dto;
using CA.ERP.Shared.Dto.Branch;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Application.CommandQuery.BranchCommandQuery.GetManyBranch
{
    public class GetManyBranchQuery : IRequest<PaginatedResponse<BranchView>>
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public Status Status { get; set; }

        public GetManyBranchQuery(int skip = 0, int take = 0, Status status = Status.Active)
        {
            Skip = skip;
            Take = take;
            Status = status;
        }
    }
}
