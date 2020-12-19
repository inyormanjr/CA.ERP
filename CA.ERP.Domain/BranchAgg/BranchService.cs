using CA.ERP.Domain.Base;
using Microsoft.Extensions.Logging;
using OneOf;
using OneOf.Types;
using System;
using System.Collections.Generic;
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

        public BranchService(ILogger<BranchService> logger, IBranchRepository branchRepository, IBranchFactory branchFactory)
        {
            _logger = logger;
            _branchRepository = branchRepository;
            _branchFactory = branchFactory;
        }

        /// <summary>
        /// Get a list of branches
        /// </summary>
        /// <returns></returns>
        public async Task<List<Branch>> GetAsync()
        {
            _logger.LogInformation($"Getting all branch");

            return await _branchRepository.GetAll();
        }

        /// <summary>
        /// Create a branch
        /// </summary>
        /// <param name="domBranch"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<OneOf<Branch, None>> CreateBranchAsync(string name, int branchNo, string code, string address, string contact, CancellationToken cancellationToken = default)
        {
            //validate branch here/ no idea how to validate yet.
            var branch = _branchFactory.Create(name, branchNo, code, address, contact);
            return await _branchRepository.AddAsync(branch, cancellationToken);
        }

        public async Task<OneOf<Branch, NotFound>> UpdateAsync(string id, Branch domBranch, CancellationToken cancellationToken)
        {
            var fromDal = await _branchRepository.UpdateAsync(id, domBranch, cancellationToken);
            return fromDal.Match<OneOf<Branch, NotFound>>(
                f0: (branch) => branch,
                f1: (none) => default(NotFound)
            );
        }

        public async Task<OneOf<Success, NotFound>> DeleteAsync(string id, CancellationToken cancellationToken)
        {
             var result = await _branchRepository.DeleteAsync(id, cancellationToken);
            return result.Match<OneOf<Success, NotFound>>(
                f0: (success) => success,
                f1: (none) => default(NotFound)
            );
        }
    }
}
