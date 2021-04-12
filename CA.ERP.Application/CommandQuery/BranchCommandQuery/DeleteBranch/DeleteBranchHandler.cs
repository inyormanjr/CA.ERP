using CA.ERP.Domain.BranchAgg;
using CA.ERP.Domain.Core;
using CA.ERP.Domain.Core.DomainResullts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Application.CommandQuery.BranchCommandQuery.DeleteBranch
{
    public class DeleteBranchHandler : IRequestHandler<DeleteBranchCommand, DomainResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBranchRepository _branchRepository;

        public DeleteBranchHandler(IUnitOfWork unitOfWork, IBranchRepository branchRepository)
        {
            _unitOfWork = unitOfWork;
            _branchRepository = branchRepository;
        }
        public async Task<DomainResult> Handle(DeleteBranchCommand request, CancellationToken cancellationToken)
        {
            await _branchRepository.DeleteAsync(request.Id, cancellationToken: cancellationToken);
            await _unitOfWork.CommitAsync();
            return DomainResult.Success();
        }
    }
}
