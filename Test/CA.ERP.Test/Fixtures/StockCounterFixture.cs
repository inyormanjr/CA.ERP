using CA.ERP.Domain.StockCounterAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.Test.Fixtures
{
    public class StockCounterFixture
    {
        public StockCounter GetStockCounter()
        {
            return StockCounter.Create("01", 2021);
        }
    }
}
