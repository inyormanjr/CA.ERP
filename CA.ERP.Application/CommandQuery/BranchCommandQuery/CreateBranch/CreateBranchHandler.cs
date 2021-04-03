using CA.ERP.Domain.BranchAgg;
using CA.ERP.Domain.Core;
using CA.ERP.Domain.Core.DomainResullts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Application.CommandQuery.BranchCommandQuery.CreateBranch
{
    public class CreateBranchHandler : IRequestHandler<CreateBranchCommand, DomainResult<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBranchRepository _branchRepository;

        public CreateBranchHandler(IUnitOfWork unitOfWork, IBranchRepository branchRepository)
        {
            _unitOfWork = unitOfWork;
            _branchRepository = branchRepository;
        }
        public async Task<DomainResult<Guid>> Handle(CreateBranchCommand request, CancellationToken cancellationToken)
        {
            DomainResult<Guid> ret;

            var result = Branch.Create(request.Name, request.BranchNo, request.Code, request.Address, request.Contact);
            if (result.IsSuccess)
            {
                Branch branch = result.Result;

                var id = await _branchRepository.AddAsync(branch, cancellationToken);
                ret = DomainResult<Guid>.Success(id);
            }
            else
            {
                ret = result.ConvertTo<Guid>();
            }
            await _unitOfWork.CommitAsync();
            return ret;
        }
    }
}
