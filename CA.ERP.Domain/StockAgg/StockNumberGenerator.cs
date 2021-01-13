using CA.ERP.Domain.BranchAgg;
using CA.ERP.Domain.Helpers;
using CA.ERP.Domain.StockReceiveAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.Domain.StockAgg
{
    public class StockNumberGenerator : IStockNumberGenerator
    {
        private readonly IDateTimeHelper _dateTimeHelper;

        public StockNumberGenerator(IDateTimeHelper dateTimeHelper)
        {
            _dateTimeHelper = dateTimeHelper;
        }

        public Task<IEnumerable<string>> GenerateStockNumberAsync(StockCounter stockCounter, int count)
        {

            string prefix = $"{stockCounter.Code}{(_dateTimeHelper.GetCurrentYear() - 2000).ToString("D2")}";
            return Task.FromResult(generateStockNumber(prefix, stockCounter.Counter, count));
        }

        private IEnumerable<string> generateStockNumber(string prefix, int starting, int count)
        {
            string format = $"D5";
            for (int i = 1; i <= count; i++)
            {
                yield return $"{prefix}{(starting+i).ToString(format)}";
            }
        }
    }
}
