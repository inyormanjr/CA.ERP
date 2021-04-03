using CA.ERP.Domain.BranchAgg;
using CA.ERP.Domain.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Application.CommandQuery.BranchCommandQuery.GetManyBranch
{
    public class GetManyBranchHandler : IRequestHandler<GetManyBranchQuery, PaginatedList<Branch>>
    {
        private readonly IBranchRepository _branchRepository;

        public GetManyBranchHandler(IBranchRepository branchRepository)
        {
            _branchRepository = branchRepository;
        }

        public async Task<PaginatedList<Branch>> Handle(GetManyBranchQuery request, CancellationToken cancellationToken)
        {
            var branches = await _branchRepository.GetManyAsync(request.Skip, request.Take, request.Status, cancellationToken: cancellationToken);
            var total = await _branchRepository.Count(request.Status, cancellationToken: cancellationToken);
            return new PaginatedList<Branch>() {
                 Data = branches,
                 TotalCount = total
            };
        }
    }
}
