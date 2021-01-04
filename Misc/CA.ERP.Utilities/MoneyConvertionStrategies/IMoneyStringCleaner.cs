using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.Utilities.MoneyConvertionStrategies
{
    public interface IMoneyStringCleaner
    {
        string CleanMoneyString(string moneyString);
    }
}
