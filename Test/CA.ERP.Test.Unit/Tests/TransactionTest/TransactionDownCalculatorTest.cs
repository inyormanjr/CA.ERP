using System;
using System.Collections.Generic;
using CA.ERP.Domain.TransactionAgg;
using CA.ERP.Domain.TransactionAgg.Caculators;
using FluentAssertions;
using Xunit;

namespace CA.ERP.Test.Unit.Tests.TransactionTest
{
    public class TransactionDownCalculatorTest
    {
        [Theory]
        [MemberData(nameof(GetData))]
        public void ShouldCalculateTransactionDownCorrect(decimal[] productDowns, decimal expectedTotal)
        {
            TransactionDownCalculator sut = new TransactionDownCalculator();

            


            var transaction = new Transaction()
            {
               Total = expectedTotal,
            };

            foreach (var productDown in productDowns)
            {
                var product = new TransactionProduct() {
                    SalePrice = productDown * 10,
                    DownPayment = productDown
                };
                transaction.TransactionProducts.Add(product);
            }


            var total = sut.Calculate(transaction);

            total.Should().Be(transaction.Total);
        }

        public static IEnumerable<object[]> GetData()
        {
            var allData = new List<object[]>
            {
                new object[] { new decimal[]{2000, 2500}, 4500},
                new object[] { new decimal[]{1000, 3600}, 4600},
            };

            return allData;
        }
    }
}