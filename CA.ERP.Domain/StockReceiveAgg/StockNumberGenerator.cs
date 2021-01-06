using CA.ERP.Domain.BranchAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.StockReceiveAgg
{
    public class StockNumberGenerator : IStockNumberGenerator
    {
        private readonly IBranchRepository _branchRepository;

        public StockNumberGenerator(IBranchRepository branchRepository)
        {
            _branchRepository = branchRepository;
        }
        public List<string> GenerateStockNumber(Guid brancId, int count)
        {
            throw new NotImplementedException();
        }
    }
}
