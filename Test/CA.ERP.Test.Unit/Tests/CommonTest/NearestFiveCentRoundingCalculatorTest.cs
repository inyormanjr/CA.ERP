using CA.ERP.Domain.Common.Rounding;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CA.ERP.Test.Unit.Tests.CommonTest
{
    public class NearestFiveCentRoundingCalculatorTest
    {
        [Theory]
        [InlineData(0.01, 0.00)]
        [InlineData(0.40, 0.40)]
        [InlineData(0.03, 0.05)]
        [InlineData(1.01, 1.00)]
        [InlineData(0.00, 0.00)]
        public void ShouldRoundToNearestFiveCentsSuccess(decimal toRound, decimal expected)
        {
            NearestFiveCentRoundingCalculator nearestFiveCentRoundingCalculator = new NearestFiveCentRoundingCalculator();
            decimal actual = nearestFiveCentRoundingCalculator.Round(toRound);
            actual.Should().Be(expected);
        }
    }
}
