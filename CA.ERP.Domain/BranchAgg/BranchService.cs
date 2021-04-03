using CA.ERP.Domain.Base;
using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Domain.UnitOfWorkAgg;
using CA.ERP.Domain.UserAgg;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using OneOf;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Domain.BranchAgg
{
    public class BranchService : ServiceBase<Branch>
    {
        private readonly ILogger<BranchService> _logger;
        private readonly IBranchRepository _branchRepository;

        public BranchService(IUnitOfWork unitOfWork, ILogger<BranchService> logger, IBranchRepository branchRepository, IValidator<Branch> branchValidator, IUserHelper userHelper)
            :base(unitOfWork, branchRepository, branchValidator, userHelper)
        {
            _logger = logger;
            _branchRepository = branchRepository;
        }


        /// <summary>
        /// Create a branch
        /// </summary>
        /// <returns></returns>
        public async Task<DomainResult<Guid>> CreateBranchAsync(string name, int branchNo, string code, string address, string contact, CancellationToken cancellationToken = default)
        {
            DomainResult<Guid> ret;
            //validate branch here/ no idea how to validate yet.

            var result = Branch.Create(name, branchNo, code, address, contact);
            if (result.IsSuccess)
            {
                Branch branch = result.Result;
                branch.CreatedBy = _userHelper.GetCurrentUserId();
                branch.UpdatedBy = _userHelper.GetCurrentUserId();
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

        public async Task<List<Branch>> GetCurrentUserBranches(CancellationToken cancellationToken = default)
        {
            return await _branchRepository.GetManyByUserIdAsync(_userHelper.GetCurrentUserId(), cancellationToken);
        }


    }
}
