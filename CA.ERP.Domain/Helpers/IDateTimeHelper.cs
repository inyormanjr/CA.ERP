using CA.ERP.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.Helpers
{
    public interface IDateTimeHelper : IHelper
    {
        DateTimeOffset GetCurrentDateTimeOffset();
        DateTime GetCurrentDateTime();
    }
}
