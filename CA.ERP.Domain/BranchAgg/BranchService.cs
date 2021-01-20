using CA.ERP.Domain.Base;
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
        private readonly IBranchFactory _branchFactory;
        private readonly IValidator<Branch> _branchValidator;

        public BranchService(IUnitOfWork unitOfWork, ILogger<BranchService> logger, IBranchRepository branchRepository, IBranchFactory branchFactory, IValidator<Branch> branchValidator, IUserHelper userHelper)
            :base(unitOfWork, branchRepository, branchValidator, userHelper)
        {
            _logger = logger;
            _branchRepository = branchRepository;
            _branchFactory = branchFactory;
            _branchValidator = branchValidator;
        }


        /// <summary>
        /// Create a branch
        /// </summary>
        /// <param name="domBranch"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<OneOf<Guid, List<ValidationFailure>>> CreateBranchAsync(string name, int branchNo, string code, string address, string contact, CancellationToken cancellationToken = default)
        {
            OneOf<Guid, List<ValidationFailure>> ret;
            //validate branch here/ no idea how to validate yet.
            var branch = _branchFactory.Create(name, branchNo, code, address, contact);
            var validationResult = _branchValidator.Validate(branch);
            if (!validationResult.IsValid)
            {
                ret = validationResult.Errors.ToList();
            }
            else
            {
                branch.CreatedBy = _userHelper.GetCurrentUserId();
                branch.UpdatedBy = _userHelper.GetCurrentUserId();
                ret = await _branchRepository.AddAsync(branch, cancellationToken);
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
