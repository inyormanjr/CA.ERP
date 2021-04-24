using CA.ERP.Common.ErrorCodes;
using CA.ERP.Common.Types;
using CA.ERP.Domain.Core;
using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Domain.PurchaseOrderAgg;
using CA.ERP.Domain.Services;
using CA.ERP.Domain.StockAgg;
using CA.ERP.Domain.StockReceiveAgg;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Application.CommandQuery.StockReceiveCommandQuery.CommitStockReceive
{
    public class CommitStockReceiveHandler : IRequestHandler<CommitStockReceiveCommand, DomainResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStockRepository _stockRepository;
        private readonly IStockReceiveRepository _stockReceiveRepository;
        private readonly ICommitStockReceiveFromPurchaseOrderService _commitStockReceiveFromPurchaseOrderService;
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;

        public CommitStockReceiveHandler(IUnitOfWork unitOfWork,IStockRepository stockRepository, IStockReceiveRepository stockReceiveRepository, ICommitStockReceiveFromPurchaseOrderService commitStockReceiveFromPurchaseOrderService, IPurchaseOrderRepository purchaseOrderRepository)
        {
            _unitOfWork = unitOfWork;
            _stockRepository = stockRepository;
            _stockReceiveRepository = stockReceiveRepository;
            _commitStockReceiveFromPurchaseOrderService = commitStockReceiveFromPurchaseOrderService;
            _purchaseOrderRepository = purchaseOrderRepository;
        }
        public async Task<DomainResult> Handle(CommitStockReceiveCommand request, CancellationToken cancellationToken)
        {
            var stockReceive = await _stockReceiveRepository.GetByIdWithItemsAsync(request.Id, cancellationToken);
            if (stockReceive == null)
            {
                return DomainResult.Error(ErrorType.NotFound, StockReceiveErrorCodes.NotFound, "Stock receive was not found.");
            }
            if (stockReceive.IsCommitted())
            {
                return DomainResult.Error(StockReceiveErrorCodes.AlreadyCommitted, "Stock receive can not be edited because it's already committed.");
            }
            if (request.StockReceive == null)
            {
                return DomainResult.Error(ErrorType.NotFound, StockReceiveErrorCodes.NotFound, "Stock receive dto was not found.");
            }
            foreach (var item in stockReceive.Items)
            {
                var itemToCommit = request.StockReceive.Items.FirstOrDefault(i => i.Id == item.Id);
                if (itemToCommit != null)
                {
                    item.Commit(itemToCommit.Status, itemToCommit.SerialNumber);
                }
            }
            switch (stockReceive.StockSouce)
            {
                case StockSource.PurchaseOrder:
                    var commitResult = await CommitFromPurchaseOrderAsync(stockReceive, cancellationToken);
                    if (!commitResult.IsSuccess)
                    {
                        return commitResult;
                    }
                    break;
                default:
                    throw new NotImplementedException($"Can't handle stock source of {stockReceive.StockSouce}");
            }

            await _stockReceiveRepository.UpdateAsync(request.Id, stockReceive);


            await _unitOfWork.CommitAsync(cancellationToken);
            return DomainResult.Success();
        }

        public async Task<DomainResult> CommitFromPurchaseOrderAsync(StockReceive stockReceive, CancellationToken cancellationToken)
        {
            var purchaseOrder = await _purchaseOrderRepository.GetByIdWithPurchaseOrderItemsAsync(stockReceive.PurchaseOrderId ?? Guid.Empty, cancellationToken);
            if (purchaseOrder == null)
            {
                return DomainResult.Error(ErrorType.NotFound, PurchaseOrderErrorCodes.NotFound, "Purchase error");
            }

            var commitStockReceiveResult = _commitStockReceiveFromPurchaseOrderService.Commit(purchaseOrder, stockReceive);
            if (!commitStockReceiveResult.IsSuccess)
            {
                return commitStockReceiveResult;
            }
            foreach (var stock in commitStockReceiveResult.Result)
            {
                await _stockRepository.AddAsync(stock);
            }
            return DomainResult.Success();
        }
    }
}
