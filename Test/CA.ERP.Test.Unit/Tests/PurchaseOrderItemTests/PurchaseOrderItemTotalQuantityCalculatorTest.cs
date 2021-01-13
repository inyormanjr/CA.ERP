using CA.ERP.Domain.PurchaseOrderAgg;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CA.ERP.Test.Unit.Tests.PurchaseOrderItemTests
{
    public class PurchaseOrderItemTotalQuantityCalculatorTest
    {
        [Theory]
        [MemberData(nameof(GetData))]
        public void ShouldComputePurchaseOrderItemTotalQuantitySuccess(PurchaseOrderItem purchaseOrderItem, decimal expected)
        {
            var sut = new PurchaseOrderItemTotalQuantityCalculator();
            decimal actual = sut.Calculate(purchaseOrderItem);
            actual.Should().Be(expected);
        }

        public static IEnumerable<object[]> GetData()
        {
            var allData = new List<object[]>
            {
                new object[] { new PurchaseOrderItem() { FreeQuantity = 10, OrderedQuantity = 50 }, 60 },
                new object[] { new PurchaseOrderItem() { FreeQuantity = 1, OrderedQuantity = 3.25m }, 4.25 },
                new object[] { new PurchaseOrderItem() { FreeQuantity = 2, OrderedQuantity = 5 }, 7 },
                new object[] { new PurchaseOrderItem() { FreeQuantity = 5, OrderedQuantity = 4 }, 9 },
            };

            return allData;
        }

    }
}
