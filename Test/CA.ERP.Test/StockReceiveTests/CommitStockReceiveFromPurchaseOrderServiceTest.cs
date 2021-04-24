using CA.ERP.Common.Types;
using CA.ERP.Domain.Services;
using CA.ERP.Domain.StockReceiveAgg;
using CA.ERP.Test.Fixtures;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CA.ERP.Test.StockReceiveTests
{
    public class CommitStockReceiveFromPurchaseOrderServiceTest : IClassFixture<PurchaseOrderFixture>, IClassFixture<DateTimeProviderFixture>, IClassFixture<StockReceiveFixture>
    {
        private readonly PurchaseOrderFixture _purchaseOrderFixture;
        private readonly DateTimeProviderFixture _dateTimeProviderFixture;
        private readonly StockReceiveFixture _stockReceiveFixture;

        public CommitStockReceiveFromPurchaseOrderServiceTest(PurchaseOrderFixture purchaseOrderFixture, DateTimeProviderFixture dateTimeProviderFixture, StockReceiveFixture stockReceiveFixture)
        {
            _purchaseOrderFixture = purchaseOrderFixture;
            _dateTimeProviderFixture = dateTimeProviderFixture;
            _stockReceiveFixture = stockReceiveFixture;
        }

        [Fact]
        public async Task Should_Commit_StockReceive_From_PurchaseOrder_Successfully()
        {
            //arrange
            var purchaseOrder = _purchaseOrderFixture.GetPurchaseOrder(_dateTimeProviderFixture.GetDateTimeProvider());
            var stockReceive = _stockReceiveFixture.GetStockReceive(purchaseOrder, _dateTimeProviderFixture.GetDateTimeProvider());
            foreach (StockReceiveItem item in stockReceive.Items)
            {
                item.Commit(StockReceiveItemStatus.Received);
            }
            ICommitStockReceiveFromPurchaseOrderService commitStockReceiveFromPurchaseOrderService = new CommitStockReceiveFromPurchaseOrderService(_dateTimeProviderFixture.GetDateTimeProvider());

            //act
            var stocksResult = commitStockReceiveFromPurchaseOrderService.Commit(purchaseOrder, stockReceive);

            //assert
            stocksResult.IsSuccess.Should().BeTrue();

            var stocks = stocksResult.Result;

            stockReceive.Stage.Should().Be(StockReceiveStage.Commited);
            stockReceive.DateReceived.Should().NotBeNull();
            stockReceive.DateReceived.Should().Be(_dateTimeProviderFixture.GetDateTimeProvider().GetCurrentDateTimeOffset());
            stockReceive.Items.Count.Should().BeGreaterThan(0);

            foreach (var purchaseOrderItem in purchaseOrder.PurchaseOrderItems)
            {
                var receiveStocks = stocks.Where(i => i.PurchaseOrderItemId == purchaseOrderItem.Id);
                receiveStocks.Count().Should().Be(purchaseOrderItem.DeliveredQuantity);
                foreach (var stock in receiveStocks)
                {
                    stock.MasterProductId.Should().NotBeEmpty();
                    stock.StockStatus.Should().NotBe(StockStatus.Unknown);
                }
            }
        }
    }
}
