using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.Core
{
    public interface IDateTimeProvider
    {
        DateTimeOffset GetCurrentDateTimeOffset();
    }
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTimeOffset GetCurrentDateTimeOffset()
        {
            return DateTimeOffset.Now;
        }
    }


}
