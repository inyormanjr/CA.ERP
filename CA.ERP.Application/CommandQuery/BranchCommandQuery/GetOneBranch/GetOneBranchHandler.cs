using CA.ERP.Domain.BranchAgg;
using CA.ERP.Domain.Core.DomainResullts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Application.CommandQuery.BranchCommandQuery.GetOneBranch
{
    public class GetOneBranchHandler : IRequestHandler<GetOneBranchQuery, DomainResult<Branch>>
    {
        private readonly IBranchRepository _branchRepository;

        public GetOneBranchHandler(IBranchRepository branchRepository)
        {
            _branchRepository = branchRepository;
        }
        public async Task<DomainResult<Branch>> Handle(GetOneBranchQuery request, CancellationToken cancellationToken)
        {
            var branch = await _branchRepository.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
            if (branch == null)
            {
                return DomainResult<Branch>.Error(ErrorType.NotFound, BranchErrorCodes.NotFound, "Branch not found");
            }
            return DomainResult<Branch>.Success(branch);
        }
    }
}
