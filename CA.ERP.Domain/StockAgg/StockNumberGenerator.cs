using CA.ERP.Domain.BranchAgg;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.Domain.StockAgg
{
    public class StockNumberGenerator : IStockNumberGenerator
    {
        public StockNumberGenerator()
        {
        }
        public Task<IEnumerable<string>> GenerateStockNumberAsync(string prefix, string starting, int count)
        {
            return Task.FromResult(generateStockNumber(prefix, starting, count));
        }

        private IEnumerable<string> generateStockNumber(string prefix, string starting, int count)
        {
            int intStarting = int.Parse(starting);
            string format = $"D{starting.Length}";
            for (int i = 0; i < count; i++)
            {
                yield return $"{prefix}{intStarting++.ToString(format)}";
            }
        }
    }
}
