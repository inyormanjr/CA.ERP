using CA.ERP.Domain.Core;
using CA.ERP.Domain.Core.DomainResullts;
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
        private readonly IIdentityProvider _identityProvider;

        public CreatePurchaseOrderHandler(IUnitOfWork unitOfWork, IPurchaseOrderRepository purchaseOrderRepository, IDateTimeProvider dateTimeProvider, IPurchaseOrderBarcodeGenerator purchaseOrderBarcodeGenerator, ISupplierMasterProductRepository supplierMasterProductRepository)
        {
            _unitOfWork = unitOfWork;
            _purchaseOrderRepository = purchaseOrderRepository;
            _dateTimeProvider = dateTimeProvider;
            _purchaseOrderBarcodeGenerator = purchaseOrderBarcodeGenerator;
            _supplierMasterProductRepository = supplierMasterProductRepository;
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
            

            //update supplier prices
            foreach (var item in purchaseOrder.PurchaseOrderItems)
            {
                var createSupplierMasterProduct = SupplierMasterProduct.Create(item.MasterProductId, request.SupplierId, item.CostPrice);
                if (createSupplierMasterProduct.IsSuccess)
                {
                    await _supplierMasterProductRepository.AddOrUpdateAsync(createSupplierMasterProduct.Result, cancellationToken);
                }
                
            }
            await _unitOfWork.CommitAsync();

            return DomainResult<Guid>.Success(id);
        }
    }
}
