using CA.ERP.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.StockReceiveAgg
{
    public interface IStockNumberGenerator: IHelper
    {
        /// <summary>
        /// Generate stock number for given branch
        /// </summary>
        /// <param name="brancId"></param>
        /// <param name="count">number of stock number to generate</param>
        /// <returns></returns>
        List<string> GenerateStockNumber(Guid brancId, int count);
    }
}
