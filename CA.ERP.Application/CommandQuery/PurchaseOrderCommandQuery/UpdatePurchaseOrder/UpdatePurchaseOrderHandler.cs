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

namespace CA.ERP.Application.CommandQuery.PurchaseOrderCommandQuery.UpdatePurchaseOrder
{
    public class UpdatePurchaseOrderHandler : IRequestHandler<UpdatePurchaseOrderCommand, DomainResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;
        private readonly IIdentityProvider _identityProvider;
        private readonly IDateTimeProvider _dateTimeProvider;

        public UpdatePurchaseOrderHandler(IUnitOfWork unitOfWork, IPurchaseOrderRepository purchaseOrderRepository, IIdentityProvider identityProvider, IDateTimeProvider dateTimeProvider)
        {
            _unitOfWork = unitOfWork;
            _purchaseOrderRepository = purchaseOrderRepository;
            _identityProvider = identityProvider;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<DomainResult> Handle(UpdatePurchaseOrderCommand request, CancellationToken cancellationToken)
        {
            PurchaseOrder purchaseOrder = await _purchaseOrderRepository.GetByIdAsync(request.Id, cancellationToken);

            if (purchaseOrder == null)
            {
                return DomainResult.Error(ErrorType.NotFound, PurchaseOrderErrorCodes.NotFound, "Purchase order was not found");
            }
            var currentIdentity = await _identityProvider.GetCurrentIdentity();

            var updateResult = purchaseOrder.Update(request.DeliveryDate, currentIdentity?.Id ?? Guid.Empty, request.SupplierId, request.DesntinationBranchId, _dateTimeProvider);
            if (!updateResult.IsSuccess)
            {
                return updateResult;
            }
            await _purchaseOrderRepository.UpdateAsync(request.Id, purchaseOrder);


            await _unitOfWork.CommitAsync();

            return DomainResult.Success();
        }
    }
}
