using CA.ERP.Domain.Common.Rounding;
using CA.ERP.Domain.PurchaseOrderAgg;
using FluentAssertions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CA.ERP.Test.Unit.Tests.PurchaseOrderTest
{
    public class PurchaseOrderTotalCostPriceCalculatorTest
    {
        [Theory]
        [ClassData(typeof(PurchaseOrderTotalCostPriceCalculatorClassData))]
        public void ShouldCalculateTotalCostPrice(PurchaseOrder purchaseOrder, ICollection<PurchaseOrderItem> purchaseOrderItems, decimal expected)
        {
            var sut = new PurchaseOrderTotalCostPriceCalculator(new NearestFiveCentRoundingCalculator());
            var actual = sut.Calculate(purchaseOrder, purchaseOrderItems);

            actual.Should().Be(expected);
        }
    }
}
