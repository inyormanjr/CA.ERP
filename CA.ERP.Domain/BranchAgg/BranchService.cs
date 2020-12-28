using CA.ERP.Domain.Base;
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
    public class BranchService : ServiceBase
    {
        private readonly ILogger<BranchService> _logger;
        private readonly IBranchRepository _branchRepository;
        private readonly IBranchFactory _branchFactory;
        private readonly IValidator<Branch> _branchValidator;

        public BranchService(ILogger<BranchService> logger, IBranchRepository branchRepository, IBranchFactory branchFactory, IValidator<Branch> branchValidator)
        {
            _logger = logger;
            _branchRepository = branchRepository;
            _branchFactory = branchFactory;
            _branchValidator = branchValidator;
        }

        /// <summary>
        /// Get a list of branches
        /// </summary>
        /// <returns></returns>
        public async Task<List<Branch>> GetAsync()
        {
            _logger.LogInformation($"Getting all branch");

            return await _branchRepository.GetManyAsync();
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
                ret = await _branchRepository.AddAsync(branch, cancellationToken);
            }
            return ret;
        }

        public async Task<OneOf<Guid, List<ValidationFailure>, NotFound>> UpdateAsync(Guid id, Branch domBranch, CancellationToken cancellationToken)
        {
            OneOf<Guid, List<ValidationFailure>, NotFound> ret;
            var validationResult = _branchValidator.Validate(domBranch);
            if (!validationResult.IsValid)
            {
                ret = validationResult.Errors.ToList();
            }
            else
            {

                var fromDal = await _branchRepository.UpdateAsync(id, domBranch, cancellationToken);
                ret = fromDal.Match<OneOf<Guid, List<ValidationFailure>, NotFound>>(
                    f0: (branchId) => branchId,
                    f1: (none) => default(NotFound)
                );
            }
            return ret;
        }

        public async Task<OneOf<Success, NotFound>> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
             var result = await _branchRepository.DeleteAsync(id, cancellationToken);
            return result.Match<OneOf<Success, NotFound>>(
                f0: (success) => success,
                f1: (none) => default(NotFound)
            );
        }
    }
}
