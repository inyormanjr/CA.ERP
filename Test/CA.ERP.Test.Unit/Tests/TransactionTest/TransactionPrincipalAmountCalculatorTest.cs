using System.Collections.Generic;
using CA.ERP.Domain.TransactionAgg;
using CA.ERP.Domain.TransactionAgg.Caculators;
using FluentAssertions;
using Xunit;

namespace CA.ERP.Test.Unit.Tests.TransactionTest
{
    public class TransactionPrincipalAmountCalculatorTest
    {
        [Theory]
        [MemberData(nameof(GetData))]
        public void ShouldCalculateTransactionPrincipalAmountCorrect(int terms, decimal grossMonthly, decimal expectedPricipalAmount)
        {
            TransactionPrincipalAmountCalculator sut = new TransactionPrincipalAmountCalculator();

            var transaction = new Transaction()
            {
                GrossMonthly = grossMonthly,
                Terms = terms,
                PrincipalAmount = expectedPricipalAmount
            };

            var actualPrincipalAmount = sut.Calculate(transaction);

            actualPrincipalAmount.Should().Be(transaction.PrincipalAmount);
        }

        public static IEnumerable<object[]> GetData()
        {
            var allData = new List<object[]>
            {
                new object[] { 12, 1000, 12000},
                new object[] { 10, 600, 6000},
            };

            return allData;
        }
    }
}