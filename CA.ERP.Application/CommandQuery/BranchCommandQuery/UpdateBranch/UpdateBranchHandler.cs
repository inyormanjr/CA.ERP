using CA.ERP.Domain.BranchAgg;
using CA.ERP.Domain.Core;
using CA.ERP.Domain.Core.DomainResullts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Application.CommandQuery.BranchCommandQuery.UpdateBranch
{
    public class UpdateBranchHandler : IRequestHandler<UpdateBranchCommand, DomainResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBranchRepository _branchRepository;

        public UpdateBranchHandler(IUnitOfWork unitOfWork, IBranchRepository branchRepository)
        {
            _unitOfWork = unitOfWork;
            _branchRepository = branchRepository;
        }

        public async Task<DomainResult> Handle(UpdateBranchCommand request, CancellationToken cancellationToken)
        {
            var branch = await _branchRepository.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
            if (branch == null)
            {
                return DomainResult<Branch>.Error(ErrorType.NotFound, BranchErrorCodes.NotFound, "Branch not found");
            }

            branch.Update(request.Name, request.BranchNo, request.Code, request.Address, request.Contact);

            await _branchRepository.UpdateAsync(request.Id, branch, cancellationToken: cancellationToken);

            await _unitOfWork.CommitAsync();

            return DomainResult.Success();
        }
    }
}
