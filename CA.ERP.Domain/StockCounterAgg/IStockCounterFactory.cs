using CA.ERP.Domain.Base;
using CA.ERP.Domain.StockReceiveAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.StockCounterAgg
{
    public interface IStockCounterFactory: IFactory<StockCounter>
    {
        StockCounter CreateFresh(string code);
    }
}
