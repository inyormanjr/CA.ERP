using CA.ERP.Domain.Helpers;
using CA.ERP.Domain.StockAgg;
using CA.ERP.Domain.StockReceiveAgg;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CA.ERP.Test.Unit.Tests.StockTest
{
    public class StockNumberGeneratorTest
    {
        [Theory]
        [MemberData(nameof(GetData), parameters: 3)]
        public async Task ShouldGenerateBarcodeSuccess(StockCounter stockCounter,int year, int count, int expectedStart)
        {
            var dateTimeMock = new Mock<IDateTimeHelper>();
            dateTimeMock.Setup(foo => foo.GetCurrentYear()).Returns(year);

            var stockCodeMock = new Mock<IStockCounterRepository>();

            var stockNumberGenerator = new StockNumberGenerator(dateTimeMock.Object);


            List<string> actualStockNumbers = (await stockNumberGenerator.GenerateStockNumberAsync(stockCounter, count)).ToList();
            actualStockNumbers.Should().HaveCount(10);
            for (int i = 0; i < count; i++)
            {
                actualStockNumbers[i].Should().Be($"{stockCounter.Code}{(year-2000).ToString("00")}{(expectedStart + i).ToString("00000")}");
            }
        }

        public static IEnumerable<object[]> GetData(int numTests)
        {
            var allData = new List<object[]>
        {
            new object[] { new StockCounter() { Code = "B", Counter = 10, Year = 2020 }, 2020, 10, 11},
            new object[] { new StockCounter() { Code = "B", Counter = 0, Year = 2021 }, 2020, 10, 1 },
            new object[] { new StockCounter() { Code = "Y", Counter = 500, Year = 2020 }, 2020, 10,  501},
        };

            return allData.Take(numTests);
        }

    }
}
