using CA.ERP.Domain.Base;
using CA.ERP.Domain.StockReceiveAgg;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.Domain.StockAgg
{
    public interface IStockNumberGenerator: IHelper
    {
        /// <summary>
        /// Generate stock number for given branch
        /// </summary>
        /// <param name="brancId"></param>
        /// <param name="count">number of stock number to generate</param>
        /// <returns></returns>
        Task<IEnumerable<string>> GenerateStockNumberAsync(StockCounter stockCounter, int count);
    }
}
