using CA.ERP.Domain.PaymentAgg;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CA.ERP.Test.Unit.Tests.PaymentTest
{
    public class PaymentNetTotalCalculatorTest
    {
        [Theory]
        [MemberData(nameof(GetData))]
        public void ShouldComputePaymentNetAmountSuccess(decimal grossAmount, decimal rebate, decimal interest, decimal discount, decimal expected)
        {
            var sut = new PaymentNetTotalCalculator();
            var payment = new Payment()
            {
                GrossAmount = grossAmount,
                Rebate = rebate,
                Interest = interest,
                Discount = discount
            };
            sut.Calculate(payment);
            payment.NetAmount.Should().Be(expected);
        }

        public static IEnumerable<object[]> GetData()
        {
            var allData = new List<object[]>
            {
                new object[] { 1000, 10, 20, 200, 810 },
                new object[] { 200, 10, 20, 200, 10 },
                new object[] { 500, 50, 30, 75, 405 },
                new object[] { 500, 50, 60, 100, 410 },
            };

            return allData;
        }
    }
}
