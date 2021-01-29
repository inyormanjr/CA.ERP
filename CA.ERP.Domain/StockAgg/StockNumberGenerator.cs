using CA.ERP.Domain.BranchAgg;
using CA.ERP.Domain.Helpers;
using CA.ERP.Domain.StockReceiveAgg;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.Domain.StockAgg
{
    public class StockNumberGenerator : IStockNumberGenerator
    {
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly StockCounter _stockCounter;
        private readonly IStockCounterRepository _stockCounterRepository;

        public StockCounter StockCounter { get => _stockCounter; }

        public StockNumberGenerator(IDateTimeHelper dateTimeHelper, StockCounter stockCounter)
        {
            _dateTimeHelper = dateTimeHelper;
            _stockCounter = stockCounter;
        }

        public Task<IEnumerable<string>> GenerateStockNumberAsync()
        {

            string prefix = $"{_stockCounter.Code}{(_dateTimeHelper.GetCurrentYear() - 2000).ToString("D2")}";
            return Task.FromResult(generateStockNumber(prefix));
        }

  
        private IEnumerable<string> generateStockNumber(string prefix)
        {
            string format = $"D5";
            while(true){
                _stockCounter.Counter += 1;
                yield return $"{prefix}{(_stockCounter.Counter).ToString(format)}";
            }
            
        }

  }
}
