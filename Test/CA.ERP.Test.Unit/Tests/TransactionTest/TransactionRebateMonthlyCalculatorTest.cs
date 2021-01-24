using CA.ERP.Domain.TransactionAgg;
using CA.ERP.Domain.TransactionAgg.Caculators;
using Xunit;
using FakeItEasy;
using CA.ERP.Domain.RebateMonthlyAgg;
using FluentAssertions;
using System.Collections.Generic;

namespace CA.ERP.Test.Unit.Tests.TransactionTest
{
    public class TransactionRebateMonthlyCalculatorTest
    {
        [Theory]
        [MemberData(nameof(GetData))]
        public void ShouldCalculateTransactionRebateMonthlyCorrect(int netMonthly,  decimal expectedRebateMonthly)
        {
            var rebateMontlyRepository = A.Fake<IRebateMonthlyRepository>();
            var monthlyRebates = new List<RebateMonthly>() {
                new RebateMonthly() { Rebate = 30, Min = 0, Max = 1500},
                new RebateMonthly() { Rebate = 40, Min = 1501, Max = 3000},
                new RebateMonthly() { Rebate = 60, Min = 3001, Max = 99999999},
            };

            A.CallTo(()=>rebateMontlyRepository.GetRebateMonthlyList()).Returns(monthlyRebates);


            TransactionRebateMonthlyCalculator sut = new TransactionRebateMonthlyCalculator(rebateMontlyRepository);

            var transaction = new Transaction()
            {
                NetMonthly = netMonthly,
                RebateMonthly = expectedRebateMonthly
            };

            var actualRebateMonthly = sut.Calculate(transaction);

            actualRebateMonthly.Should().Be(transaction.RebateMonthly);
        }

        public static IEnumerable<object[]> GetData()
        {
            var allData = new List<object[]>
            {
                new object[] { 900, 30},
                new object[] { 1600, 40},
                new object[] { 4000, 60},
            };

            return allData;
        }
    }
}