using CA.ERP.Domain.Base;
using CA.ERP.Domain.Helpers;
using CA.ERP.Domain.StockReceiveAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.StockCounterAgg
{
    public class StockCounterFactory: IStockCounterFactory
    {
        private readonly IDateTimeHelper _dateTimeHelper;

        public StockCounterFactory(IDateTimeHelper dateTimeHelper)
        {
            _dateTimeHelper = dateTimeHelper;
        }
        public StockCounter CreateFresh(string code)
        {
            return new StockCounter() { Code = code, Year = _dateTimeHelper.GetCurrentYear() };
        }
    }
}
