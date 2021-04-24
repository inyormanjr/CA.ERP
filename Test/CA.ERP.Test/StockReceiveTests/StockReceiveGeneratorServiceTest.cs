using CA.ERP.Domain.Core;
using CA.ERP.Domain.PurchaseOrderAgg;
using CA.ERP.Domain.Services;
using CA.ERP.Test.Fixtures;
using FakeItEasy;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CA.ERP.Test.StockReceiveTests
{
    public class StockReceiveGeneratorServiceTest : IClassFixture<DateTimeProviderFixture>, IClassFixture<PurchaseOrderFixture>, IClassFixture<StockCounterFixture>
    {
        private readonly DateTimeProviderFixture _dateTimeProviderFixture;
        private readonly PurchaseOrderFixture _purchaseOrderFixture;
        private readonly StockCounterFixture _stockCounterFixture;

        public StockReceiveGeneratorServiceTest(DateTimeProviderFixture dateTimeProviderFixture, PurchaseOrderFixture purchaseOrderFixture, StockCounterFixture stockCounterFixture)
        {
            _dateTimeProviderFixture = dateTimeProviderFixture;
            _purchaseOrderFixture = purchaseOrderFixture;
            _stockCounterFixture = stockCounterFixture;
        }
        [Fact]
        public void Should_Generate_StockReceive_From_PurchaseOrder_Success()
        {
            //arrange
            var stockCounter = _stockCounterFixture.GetStockCounter();
            IStockNumberService stockNumberService = A.Fake<IStockNumberService>();

            A.CallTo(() => stockNumberService.GenerateStockNumbers(stockCounter)).Returns(Enumerable.Range(0, 1000).Select(i => i.ToString()));

            IStockReceiveGeneratorService sut = new StockReceiveGeneratorService(_dateTimeProviderFixture.GetDateTimeProvider(), stockNumberService);
            PurchaseOrder purchaseOrder = _purchaseOrderFixture.GetPurchaseOrder(_dateTimeProviderFixture.GetDateTimeProvider());
            var itemCostPrice = purchaseOrder.PurchaseOrderItems.FirstOrDefault().CostPrice;

            //act


            var generateStockReceiveResult = sut.FromPurchaseOrder(purchaseOrder, stockCounter);


            //assert
            generateStockReceiveResult.IsSuccess.Should().BeTrue();
            generateStockReceiveResult.Result.Items.Count.Should().Be(15);
            generateStockReceiveResult.Result.Items.Count(i => i.CostPrice == itemCostPrice).Should().Be(10);
            generateStockReceiveResult.Result.Items.Count(i => i.CostPrice == 0).Should().Be(5);

        }
    }
}
