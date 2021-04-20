using CA.ERP.Domain.Core;
using CA.ERP.Domain.StockCounterAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.Services
{
    public interface IStockNumberService
    {
        IEnumerable<string> GenerateStockNumbers(StockCounter stockCounter);
    }
    public class StockNumberService : IStockNumberService
    {
        private readonly IDateTimeProvider _dateTimeProvider;

        public StockNumberService(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
        }
        public IEnumerable<string> GenerateStockNumbers(StockCounter stockCounter)
        {

            string prefix = $"{stockCounter.Code}{(_dateTimeProvider.GetCurrentDateTimeOffset().Year - 2000).ToString("D2")}";
            return generateStockNumber(prefix, stockCounter);
        }


        private IEnumerable<string> generateStockNumber(string prefix, StockCounter stockCounter)
        {
            string format = $"D5";
            while (true)
            {
                yield return $"{prefix}{(stockCounter.GetCounterAndIncrement()).ToString(format)}";
            }

        }

    }
}
