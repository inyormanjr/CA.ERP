using CA.ERP.Domain.Common.Rounding;
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

    public class PurchaseOrderItemTotalCostPriceCalculatorTest
    {
        [Theory]
        [InlineData(100, 0, 100, 10000)]
        [InlineData(100, 25, 5, 375)]
        [InlineData(7500.75, 100.50, 3, 22200.75)]
        [InlineData(50, 25, 3, 75)]
        [InlineData(50, 25, 0, 0)]
        [InlineData(50.01, 25, 1, 25.00)]
        [InlineData(50.03, 25, 1, 25.05)]
        public void ShouldCalculateTotalCostPrice(decimal costPrice, decimal discount, decimal orderedQuantity, decimal expectedTotalCostPrice)
        {
            var purchaseOrderItem = new PurchaseOrderItem() { CostPrice = costPrice, Discount = discount, OrderedQuantity = orderedQuantity };
            var sut = new PurchaseOrderItemTotalCostPriceCalculator(new NearestFiveCentRoundingCalculator());
            decimal actualTotalCostPrice = sut.Calculate(purchaseOrderItem);

            actualTotalCostPrice.Should().Be(expectedTotalCostPrice);
        }
    }
}
