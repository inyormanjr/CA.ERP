using System;
using CA.ERP.Domain.TransactionAgg;
using Xunit;
using FluentAssertions;
using System.Collections.Generic;
using CA.ERP.Domain.TransactionAgg.Calculators;

namespace CA.ERP.Test.Unit.Tests.TransactionTest
{
    public class TransactionTotalCalculatorTest
    {
        [Theory]
        [MemberData(nameof(GetData))]
        public void ShouldCalculateTransactionTotalCorrect(decimal[] productPrices, decimal expectedTotal)
        {
            TransactionTotalCalculator sut = new TransactionTotalCalculator();

            


            var transaction = new Transaction()
            {
               Total = expectedTotal,
            };

            foreach (var productPrice in productPrices)
            {
                var product = new TransactionProduct() {
                    StockId = Guid.NewGuid(),
                    SalePrice = productPrice,
                    Remarks = "A Remark",
                };
                transaction.TransactionProducts.Add(product);
            }


            var total = sut.Calculate(transaction);

            total.Should().Be(transaction.Total);
        }

        [Fact]
        public void ShouldCalculateTransactionTotalZeroCorrect()
        {
            TransactionTotalCalculator sut = new TransactionTotalCalculator();



            var transaction = new Transaction()
            {
               Total = 0,
            };


            var total = sut.Calculate(transaction);

            total.Should().Be(transaction.Total);
        }   

        public static IEnumerable<object[]> GetData()
        {
            var allData = new List<object[]>
            {
                new object[] { new decimal[]{5000, 5000}, 10000},
                new object[] { new decimal[]{2500, 2500}, 5000},
            };

            return allData;
        }

    }
}