using CA.ERP.Domain.Base;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.Domain.BranchAgg
{
    public class BranchService : ServiceBase
    {
        private readonly ILogger<BranchService> _logger;
        private readonly IBranchRepository _branchRepository;

        public BranchService(ILogger<BranchService> logger, IBranchRepository branchRepository)
        {
            _logger = logger;
            _branchRepository = branchRepository;
        }

        public async Task<List<Branch>> GetAsync()
        {
            _logger.LogInformation($"Getting all branch");

            return await _branchRepository.GetAll();
        }
    }
}
