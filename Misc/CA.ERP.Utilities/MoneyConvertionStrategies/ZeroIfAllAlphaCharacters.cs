using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.Utilities.MoneyConvertionStrategies
{
    public class ZeroIfAllAlphaCharacters : IMoneyStringCleaner
    {
        public string CleanMoneyString(string moneyString)
        {
            return moneyString.All(char.IsLetter) ? "0" : moneyString;
        }
    }
}
