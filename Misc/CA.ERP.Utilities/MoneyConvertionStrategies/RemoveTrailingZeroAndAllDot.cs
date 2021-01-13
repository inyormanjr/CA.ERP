using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.Utilities.MoneyConvertionStrategies
{
    public class RemoveTrailingZeroAndAllDot : IMoneyStringCleaner
    {
        public string CleanMoneyString(string moneyString)
        {
            if (moneyString.EndsWith(".00"))
            {
                moneyString = moneyString.Remove(moneyString.Length - 3);
            }
            return moneyString.Replace(".", "");
        }
    }
}
