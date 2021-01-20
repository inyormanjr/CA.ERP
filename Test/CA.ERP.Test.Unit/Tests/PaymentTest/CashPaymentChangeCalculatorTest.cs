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
    public class CashPaymentChangeCalculatorTest
    {
        [Theory]
        [MemberData(nameof(GetData))]
        public void ShouldCalculateCashPaymentChangeCorrect(decimal netTotal, decimal tenderAmount, decimal expected)
        {
            var sut = new CashPaymentChangeCalculator();
            var payment = new Payment()
            {
                NetAmount = netTotal,
                CashPaymentDetail = new CashPaymentDetail() { 
                    TenderAmount = tenderAmount
                }
            };
            sut.Calculate(payment);
            payment.CashPaymentDetail.Change.Should().Be(expected);
        }

        public static IEnumerable<object[]> GetData()
        {
            var allData = new List<object[]>
            {
                new object[] { 1000, 2000, 1000 },
                new object[] { 1999, 3000, 1001 },
                new object[] { 10.50, 11, 0.50 },

            };

            return allData;
        }
    }
}
