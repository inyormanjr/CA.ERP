using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.Utilities.MoneyConvertionStrategies
{
    public class ZeroIfAllNotDigitCharacters : IMoneyStringCleaner
    {
        public string CleanMoneyString(string moneyString)
        {
            return moneyString.All(c => !char.IsDigit(c)) ? "0" : moneyString;
        }
    }
}
