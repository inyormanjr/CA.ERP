using CA.ERP.Common.ErrorCodes;
using CA.ERP.Domain.BranchAgg;
using CA.ERP.Domain.Core;
using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Domain.IdentityAgg;
using CA.ERP.Domain.PurchaseOrderAgg;
using CA.ERP.Domain.Services;
using CA.ERP.Domain.StockCounterAgg;
using CA.ERP.Domain.StockReceiveAgg;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Application.CommandQuery.StockReceiveCommandQuery.GenerateStockReceiveFromPurchaseOrder
{
    public class GenerateStockReceiveFromPurchaseOrderHandler : IRequestHandler<GenerateStockReceiveFromPurchaseOrderCommand, DomainResult<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IIdentityProvider _identityProvider;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IBranchRepository _branchRepository;
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;
        private readonly IStockReceiveGeneratorService _stockReceiveGeneratorService;
        private readonly IStockReceiveRepository _stockReceiveRepository;
        private readonly IStockCounterRepository _stockCounterRepository;

        public GenerateStockReceiveFromPurchaseOrderHandler(IUnitOfWork unitOfWork, IIdentityProvider identityProvider, IDateTimeProvider dateTimeProvider, IBranchRepository branchRepository, IPurchaseOrderRepository purchaseOrderRepository, IStockReceiveGeneratorService stockReceiveGeneratorService, IStockReceiveRepository stockReceiveRepository, IStockCounterRepository stockCounterRepository)
        {
            _unitOfWork = unitOfWork;
            _identityProvider = identityProvider;
            _dateTimeProvider = dateTimeProvider;
            _branchRepository = branchRepository;
            _purchaseOrderRepository = purchaseOrderRepository;
            _stockReceiveGeneratorService = stockReceiveGeneratorService;
            _stockReceiveRepository = stockReceiveRepository;
            _stockCounterRepository = stockCounterRepository;
        }

        public async Task<DomainResult<Guid>> Handle(GenerateStockReceiveFromPurchaseOrderCommand request, CancellationToken cancellationToken)
        {
            
            var purchaseOrder = await _purchaseOrderRepository.GetByIdWithPurchaseOrderItemsAsync(request.PurchaseOrderId, cancellationToken);
            if (purchaseOrder == null)
            {
                return DomainResult<Guid>.Error(ErrorType.NotFound, PurchaseOrderErrorCodes.NotFound, "Purchase order was not found.");
            }
            var identity = await _identityProvider.GetCurrentIdentity();
            if (!identity.BelongsToBranch(purchaseOrder.DestinationBranchId))
            {
                return DomainResult<Guid>.Error(ErrorType.Forbidden, IdentityErrorCodes.Forbidden, "Your are no assigned to this branch.");
            }
            var branch = await _branchRepository.GetByIdAsync(purchaseOrder.DestinationBranchId);
            if (branch == null)
            {
                return DomainResult<Guid>.Error(BranchErrorCodes.NotFound, "Branch was not found.");
            }

            var stockCounter = await _stockCounterRepository.GetStockCounterAsync(branch.Code, cancellationToken);
            if (stockCounter == null && (stockCounter?.IsValid(_dateTimeProvider) ?? false) == false)
            {
                stockCounter = StockCounter.Create(branch.Code, _dateTimeProvider.GetCurrentDateTimeOffset().Year);
            }

            var createStockReceiveResult =  _stockReceiveGeneratorService.FromPurchaseOrder(purchaseOrder, stockCounter);
            if (!createStockReceiveResult.IsSuccess)
            {
                return createStockReceiveResult.ConvertTo<Guid>();
            }

            var id = await _stockReceiveRepository.AddAsync(createStockReceiveResult.Result, cancellationToken);
            await _stockCounterRepository.AddOrUpdateStockCounterAsync(stockCounter, cancellationToken);
            await _purchaseOrderRepository.UpdateAsync(request.PurchaseOrderId, purchaseOrder, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            return DomainResult<Guid>.Success(id);

        }
    }
}
