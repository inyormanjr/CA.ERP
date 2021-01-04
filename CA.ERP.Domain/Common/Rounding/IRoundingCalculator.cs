using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.Common.Rounding
{
    public interface IRoundingCalculator
    {
        decimal Round(decimal toRound);
    }
}
