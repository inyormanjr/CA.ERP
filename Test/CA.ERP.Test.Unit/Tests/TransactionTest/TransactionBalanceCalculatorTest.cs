using System.Collections.Generic;
using CA.ERP.Domain.TransactionAgg;
using CA.ERP.Domain.TransactionAgg.Caculators;
using FluentAssertions;
using Xunit;

namespace CA.ERP.Test.Unit.Tests.TransactionTest
{
    public class TransactionBalanceCalculatorTest
    {
        [Theory]
        [MemberData(nameof(GetData))]
        public void ShouldCalculateTransactionBalanceCorrect(decimal total, decimal down, decimal expectedBalance)
        {
            TransactionBalanceCalculator sut = new TransactionBalanceCalculator();

            var transaction = new Transaction()
            {
               Total = total,
               Down = down
            };

            var actualBalance = sut.Calculate(transaction);

            actualBalance.Should().Be(expectedBalance);
        }

        public static IEnumerable<object[]> GetData()
        {
            var allData = new List<object[]>
            {
                new object[] { 5000, 2500, 2500},
                new object[] { 7000, 1000, 6000},
            };

            return allData;
        }

    }
}