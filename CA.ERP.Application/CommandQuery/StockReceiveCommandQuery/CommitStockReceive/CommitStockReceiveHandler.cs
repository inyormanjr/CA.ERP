using CA.ERP.Common.ErrorCodes;
using CA.ERP.Domain.Core;
using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Domain.PurchaseOrderAgg;
using CA.ERP.Domain.Services;
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
        private readonly IStockReceiveRepository _stockReceiveRepository;
        private readonly ICommitStockReceiveFromPurchaseOrderService _commitStockReceiveFromPurchaseOrderService;
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;

        public CommitStockReceiveHandler(IUnitOfWork unitOfWork,IStockReceiveRepository stockReceiveRepository, ICommitStockReceiveFromPurchaseOrderService commitStockReceiveFromPurchaseOrderService, IPurchaseOrderRepository purchaseOrderRepository)
        {
            _unitOfWork = unitOfWork;
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

            if (request.StockReceive == null)
            {
                return DomainResult.Error(ErrorType.NotFound, StockReceiveErrorCodes.NotFound, "Stock receive dto was not found.");
            }
            foreach (var item in stockReceive.Items)
            {
                var itemToCommit = request.StockReceive.Items.FirstOrDefault(i => i.Id == item.Id);
                if (itemToCommit != null)
                {
                    item.Commit(itemToCommit.StockStatus, itemToCommit.SerialNumber);
                }
            }
            if (stockReceive.StockSouce == Common.Types.StockSource.PurchaseOrder)
            {
                var purchaseOrder = await _purchaseOrderRepository.GetByIdWithPurchaseOrderItemsAsync(stockReceive.PurchaseOrderId ?? Guid.Empty, cancellationToken);
                if (purchaseOrder == null)
                {
                    return DomainResult.Error(ErrorType.NotFound, PurchaseOrderErrorCodes.NotFound, "Purchase error");
                }

                var stocks = _commitStockReceiveFromPurchaseOrderService.Commit(purchaseOrder, stockReceive);
            }
            await _unitOfWork.CommitAsync(cancellationToken);
            return DomainResult.Success();
        }
    }
}
