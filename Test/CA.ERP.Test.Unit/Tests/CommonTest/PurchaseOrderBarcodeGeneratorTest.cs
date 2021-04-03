using CA.ERP.Domain.Helpers;
using CA.ERP.Domain.PurchaseOrderAgg;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CA.ERP.Test.Unit.Tests.CommonTest
{
    public class PurchaseOrderBarcodeGeneratorTest
    {
        [Theory]
        [MemberData(nameof(GetData))]
        public void ShouldGenerateBarcodeSuccess(DateTimeOffset dateTime, string expected)
        {
            var dateTimeMock = new Mock<IDateTimeHelper>();
            dateTimeMock.Setup(foo => foo.GetCurrentDateTimeOffset()).Returns(dateTime);

            PurchaseOrderBarcodeGenerator purchaseOrderBarcodeGenerator = new PurchaseOrderBarcodeGenerator(dateTimeMock.Object);
            string actual = purchaseOrderBarcodeGenerator.GenerateBarcode();
            actual.Should().Be(expected);
        }

        public static IEnumerable<object[]> GetData()
        {
            var allData = new List<object[]>
            {
                new object[] { new DateTimeOffset(2020, 1,1,0,0,0, TimeSpan.FromSeconds(0)) , "20-00000000" },
                new object[] { new DateTimeOffset(2020, 1,1,0,1,0, TimeSpan.FromSeconds(0)) , "20-00000060" },
                new object[] { new DateTimeOffset(2020, 1,1,1,0,0, TimeSpan.FromSeconds(0)) , "20-00003600" },
                new object[] { new DateTimeOffset(2020, 1,2,0,0,0, TimeSpan.FromSeconds(0)) , "20-00086400" },
            };

            return allData;
        }
    }
}
