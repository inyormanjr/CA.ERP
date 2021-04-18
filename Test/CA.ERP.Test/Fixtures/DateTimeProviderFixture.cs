using CA.ERP.Domain.Core;
using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.Test.Fixtures
{
    public class DateTimeProviderFixture
    {
        public IDateTimeProvider GetDateTimeProvider()
        {
            DateTimeOffset dateTimeOffset = new DateTimeOffset(2021, 04, 18, 0, 0, 0, TimeSpan.Zero);

            var dateTimeProvider = A.Fake<IDateTimeProvider>();

            A.CallTo(() => dateTimeProvider.GetCurrentDateTimeOffset()).Returns(dateTimeOffset);

            return dateTimeProvider;
        }
    }
}
