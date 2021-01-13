using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.Common.Rounding
{
    public class NearestFiveCentRoundingCalculator : IRoundingCalculator
    {
        public decimal Round(decimal toRound)
        {
            toRound = toRound / 0.05m;
            toRound = Math.Round(toRound);
            return toRound * 0.05m;
        }
    }
}
