using CA.ERP.Application.CommandQuery.MasterProductCommandQuery.CreateMasterProduct;
using CA.ERP.Application.CommandQuery.StockReceiveCommandQuery.GenerateStockReceiveFromPurchaseOrder;
using CA.ERP.Domain.BranchAgg;
using CA.ERP.Domain.Core;
using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Domain.PurchaseOrderAgg;
using CA.ERP.Domain.Services;
using CA.ERP.Domain.StockCounterAgg;
using CA.ERP.Domain.StockReceiveAgg;
using CA.ERP.Test.Fixtures;
using FakeItEasy;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CA.ERP.Test.StockReceiveTests
{
    public class GenerateStockReceiveFromPurchaseOrderTest : IClassFixture<PurchaseOrderFixture>, IClassFixture<DateTimeProviderFixture>, IClassFixture<StockCounterFixture>
    {
        private readonly PurchaseOrderFixture _purchaseOrderFixture;
        private readonly DateTimeProviderFixture _dateTimeProviderFixture;
        private readonly StockCounterFixture _stockCounterFixture;

        public GenerateStockReceiveFromPurchaseOrderTest(PurchaseOrderFixture purchaseOrderFixture, DateTimeProviderFixture dateTimeProviderFixture, StockCounterFixture stockCounterFixture)
        {
            _purchaseOrderFixture = purchaseOrderFixture;
            _dateTimeProviderFixture = dateTimeProviderFixture;
            _stockCounterFixture = stockCounterFixture;
        }

        [Fact]
        public async Task Should_Generate_StockReceive_From_PurchaseOrder_Success()
        {
            //arrage

            CancellationToken cancellationToken = default;
            var purchaseOrderId = Guid.NewGuid();

            PurchaseOrder purchaseOrder = _purchaseOrderFixture.GetPurchaseOrder(_dateTimeProviderFixture.GetDateTimeProvider());

            var branchResult = Branch.Create("tempBranch", 123, "01", "address", "contact");
            var branch = branchResult.IsSuccess ? branchResult.Result : null;


            var unitOfWork = A.Fake<IUnitOfWork>();

            var stockCounter = _stockCounterFixture.GetStockCounter();

            var branchRepository = A.Fake<IBranchRepository>();
            A.CallTo(() => branchRepository.GetByIdAsync(purchaseOrder.DestinationBranchId, cancellationToken)).Returns(Task.FromResult(branch));


            var stockCounterRepository = A.Fake<IStockCounterRepository>();


            A.CallTo(() => stockCounterRepository.GetStockCounterAsync(branch.Code, cancellationToken)).Returns(Task.FromResult(stockCounter));


            IPurchaseOrderRepository purchaseOrderRepository = A.Fake<IPurchaseOrderRepository>();
            
            A.CallTo(() => purchaseOrderRepository.GetByIdWithPurchaseOrderItemsAsync(purchaseOrderId, cancellationToken)).Returns(Task.FromResult(purchaseOrder));

            IStockReceiveGeneratorService stockReceiveGeneratorService = A.Fake<IStockReceiveGeneratorService>();

            StockReceive stockReceive = new StockReceive();
            A.CallTo(() => stockReceiveGeneratorService.FromPurchaseOrder(purchaseOrder, stockCounter)).Returns(DomainResult<StockReceive>.Success(stockReceive));

            IStockReceiveRepository stockReceiveRepository = A.Fake<IStockReceiveRepository>();
            A.CallTo(() => stockReceiveRepository.AddAsync(stockReceive, cancellationToken)).Returns(Task.FromResult(Guid.NewGuid()));


            var sut = new GenerateStockReceiveFromPurchaseOrderHandler(unitOfWork, _dateTimeProviderFixture.GetDateTimeProvider(), branchRepository, purchaseOrderRepository, stockReceiveGeneratorService, stockReceiveRepository, stockCounterRepository);
            var command = new GenerateStockReceiveFromPurchaseOrderCommand(purchaseOrderId);


            //act
            var result = await sut.Handle(command, cancellationToken);

            //assert
            result.IsSuccess.Should().BeTrue();

            A.CallTo(() => stockReceiveRepository.AddAsync(stockReceive, cancellationToken)).MustHaveHappened();
            A.CallTo(() => unitOfWork.CommitAsync(cancellationToken)).MustHaveHappened();
        }
    }
}
