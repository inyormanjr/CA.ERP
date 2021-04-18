using CA.ERP.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.StockCounterAgg
{
    public class StockCounter
    {
        public string Code { get; private set; }
        public int Counter { get; private set; }
        public int Year { get; private set; }

        protected StockCounter(string code, int year)
        {
            Code = code;
            Year = year;
            Counter = 0;
        }

        public bool IsValid(IDateTimeProvider dateTimeProvider)
        {
            return dateTimeProvider.GetCurrentDateTimeOffset().Year == Year;
        }

        public int GetCounterAndIncrement()
        {
            return Counter++;
        }

        public static StockCounter Create(string code, int year)
        {
            return new StockCounter(code, year);
        }
    }
}
