using CA.ERP.Domain.BranchAgg;
using CA.ERP.Domain.Core;
using CA.ERP.Domain.Core.DomainResullts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Application.Services
{
    public interface IBranchAppService : IAppService
    {
        Task<List<Branch>> GetManyAsync(int skip = 0, int take = int.MaxValue, Status status = Status.Active, CancellationToken cancellationToken = default);
        Task<DomainResult<Branch>> GetOneAsync(Guid id, CancellationToken cancellationToken);
        Task<DomainResult<Guid>> CreateBranchAsync(string name, int branchNo, string code, string address, string contact, CancellationToken cancellationToken);
        Task<DomainResult> UpdateAsync(Guid id, string name, int branchNo, string code, string address, string contact, CancellationToken cancellationToken);
        Task<DomainResult> DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
    public class BranchAppService : IBranchAppService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<BranchAppService> _logger;
        private readonly IBranchRepository _branchRepository;

        public BranchAppService(IUnitOfWork unitOfWork, ILogger<BranchAppService> logger, IBranchRepository branchRepository)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _branchRepository = branchRepository;

        }
        public async Task<DomainResult<Guid>> CreateBranchAsync(string name, int branchNo, string code, string address, string contact, CancellationToken cancellationToken)
        {
            DomainResult<Guid> ret;
            //validate branch here/ no idea how to validate yet.

            var result = Branch.Create(name, branchNo, code, address, contact);
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

        public async Task<DomainResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            await _branchRepository.DeleteAsync(id, cancellationToken: cancellationToken);
            await _unitOfWork.CommitAsync();
            return DomainResult.Success();

        }

        public Task<List<Branch>> GetManyAsync(int skip, int take, Status status, CancellationToken cancellationToken)
        {
            return _branchRepository.GetManyAsync( cancellationToken: cancellationToken);
        }

        public async Task<DomainResult<Branch>> GetOneAsync(Guid id, CancellationToken cancellationToken)
        {
            var branch = await _branchRepository.GetByIdAsync(id, cancellationToken: cancellationToken);
            if (branch == null)
            {
                return DomainResult<Branch>.Error(ErrorType.NotFound, BranchErrorCodes.NotFound, "Branch not found");
            }
            return DomainResult<Branch>.Success(branch);
        }

        public async Task<DomainResult> UpdateAsync(Guid id, string name, int branchNo, string code, string address, string contact, CancellationToken cancellationToken)
        {
            var branch = await _branchRepository.GetByIdAsync(id, cancellationToken: cancellationToken);
            if (branch == null)
            {
                return DomainResult<Branch>.Error(ErrorType.NotFound, BranchErrorCodes.NotFound, "Branch not found");
            }

            branch.Update(name, branchNo, code, address, contact);

            await _branchRepository.UpdateAsync(id, branch, cancellationToken:cancellationToken);

            await _unitOfWork.CommitAsync();

            return DomainResult.Success();
        }

    }
}
