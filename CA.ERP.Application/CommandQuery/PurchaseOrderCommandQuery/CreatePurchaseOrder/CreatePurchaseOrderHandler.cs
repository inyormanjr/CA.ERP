using CA.ERP.Domain.Core;
using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Domain.Core.EventBus;
using CA.ERP.Domain.IdentityAgg;
using CA.ERP.Domain.PurchaseOrderAgg;
using CA.ERP.Domain.SupplierAgg;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Application.CommandQuery.PurchaseOrderCommandQuery.CreatePurchaseOrder
{
    class CreatePurchaseOrderHandler : IRequestHandler<CreatePurchaseOrderCommand, DomainResult<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IPurchaseOrderBarcodeGenerator _purchaseOrderBarcodeGenerator;
        private readonly ISupplierMasterProductRepository _supplierMasterProductRepository;
        private readonly IEventBus _eventBus;
        private readonly IIdentityProvider _identityProvider;

        public CreatePurchaseOrderHandler(IUnitOfWork unitOfWork, IIdentityProvider identityProvider, IPurchaseOrderRepository purchaseOrderRepository, IDateTimeProvider dateTimeProvider, IPurchaseOrderBarcodeGenerator purchaseOrderBarcodeGenerator, ISupplierMasterProductRepository supplierMasterProductRepository, IEventBus eventBus)
        {
            _unitOfWork = unitOfWork;
            _identityProvider = identityProvider;
            _purchaseOrderRepository = purchaseOrderRepository;
            _dateTimeProvider = dateTimeProvider;
            _purchaseOrderBarcodeGenerator = purchaseOrderBarcodeGenerator;
            _supplierMasterProductRepository = supplierMasterProductRepository;
            _eventBus = eventBus;
        }
        public async Task<DomainResult<Guid>> Handle(CreatePurchaseOrderCommand request, CancellationToken cancellationToken)
        {
            var orderedBy = await _identityProvider.GetCurrentIdentity();
            if (orderedBy == null)
            {
                return DomainResult<Guid>.Error(PurchaseOrderErrorCodes.TotalQuantityLessThanZero, "Total quantity should be greater than zero.");
            }
            var purchaseOrderResult = PurchaseOrder.Create(request.DeliveryDate, orderedBy.Id, request.SupplierId, request.DesntinationBranchId, _dateTimeProvider, _purchaseOrderBarcodeGenerator);

            if (!purchaseOrderResult.IsSuccess)
            {
                return purchaseOrderResult.ConvertTo<Guid>();
            }

            var purchaseOrder = purchaseOrderResult.Result;

            foreach (var createPurchaseOrderItem in request.PurchaseOrderItems)
            {
                var result = PurchaseOrderItem.Create(purchaseOrder.Id, createPurchaseOrderItem.MasterProductId, createPurchaseOrderItem.OrderedQuantity, createPurchaseOrderItem.FreeQuantity, createPurchaseOrderItem.CostPrice, createPurchaseOrderItem.Discount);
                if (!result.IsSuccess)
                {
                    return result.ConvertTo<Guid>();
                }
                purchaseOrder.AddPurchaseOrderItem(result.Result);
            }

            var id = await _purchaseOrderRepository.AddAsync(purchaseOrder, cancellationToken: cancellationToken);

            await _eventBus.Publish(new PurchaseOrderCreated(purchaseOrder), cancellationToken);
           
            await _unitOfWork.CommitAsync();

            return DomainResult<Guid>.Success(id);
        }
    }
}
