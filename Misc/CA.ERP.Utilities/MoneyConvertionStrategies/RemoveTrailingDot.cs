﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.Utilities.MoneyConvertionStrategies
{
    public class RemoveTrailingDot : IMoneyStringCleaner
    {
        public string CleanMoneyString(string moneyString)
        {
            return moneyString.Trim('.');
        }
    }
}
