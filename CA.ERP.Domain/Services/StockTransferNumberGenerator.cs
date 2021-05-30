using CA.ERP.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.Services
{
    public interface IStockTransferNumberGenerator
    { 
        string Generate();
    }

    public class StockTransferNumberGenerator : IStockTransferNumberGenerator
{
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly INumberToCodedStringConverter _numberToCodedStringConverter;

        public StockTransferNumberGenerator(IDateTimeProvider dateTimeProvider, INumberToCodedStringConverter numberToCodedStringConverter)
        {
            _dateTimeProvider = dateTimeProvider;
            _numberToCodedStringConverter = numberToCodedStringConverter;
        }

        public string Generate()
        {
            var dateTime = _dateTimeProvider.GetCurrentDateTimeOffset();
            var newYear = new DateTimeOffset(dateTime.Year, 1, 1, 0, 0, 0, dateTime.Offset);
            var totalMiliseconds = dateTime.ToUnixTimeMilliseconds() - newYear.ToUnixTimeMilliseconds();

            string strToEncode = string.Concat(dateTime.Year.ToString().Substring(2, 2), totalMiliseconds.ToString());
            
            return _numberToCodedStringConverter.Covert(long.Parse(strToEncode));
        }
    }
}
