using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.Helpers
{
    public class DateTimeHelper : IDateTimeHelper
    {
        public DateTime GetCurrentDateTime()
        {
            return DateTime.Now;
        }

        public DateTimeOffset GetCurrentDateTimeOffset()
        {
            return DateTimeOffset.Now;
        }
    }
}
