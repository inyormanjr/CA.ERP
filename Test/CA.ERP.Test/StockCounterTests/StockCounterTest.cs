using CA.ERP.Domain.StockCounterAgg;
using CA.ERP.Test.Fixtures;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CA.ERP.Test.StockCounterTests
{
    public class StockCounterTest: IClassFixture<DateTimeProviderFixture>
    {
        private readonly DateTimeProviderFixture _dateTimeProviderFixture;

        public StockCounterTest(DateTimeProviderFixture dateTimeProviderFixture)
        {
            _dateTimeProviderFixture = dateTimeProviderFixture;
        }
        [Fact]
        public void Should_Validate_StockCounter_Success()
        {
            StockCounter stockCounter = StockCounter.Create("01", 2021);

            bool isValid = stockCounter.IsValid(_dateTimeProviderFixture.GetDateTimeProvider());

            isValid.Should().BeTrue();

        }

        [Fact]
        public void Should_Validate_StockCounter_Fail()
        {
            StockCounter stockCounter = StockCounter.Create("01", 2020);

            bool isValid = stockCounter.IsValid(_dateTimeProviderFixture.GetDateTimeProvider());

            isValid.Should().BeFalse();

        }
    }
}
