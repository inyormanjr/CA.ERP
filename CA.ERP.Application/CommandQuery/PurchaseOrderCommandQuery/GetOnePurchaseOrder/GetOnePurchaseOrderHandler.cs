using CA.ERP.Domain.Core;
using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Domain.PurchaseOrderAgg;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Application.CommandQuery.PurchaseOrderCommandQuery.GetOnePurchaseOrder
{
    public class GetOnePurchaseOrderHandler : IRequestHandler<GetOnePurchaseOrderQuery, DomainResult<PurchaseOrder>>
    {
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;

        public GetOnePurchaseOrderHandler( IPurchaseOrderRepository purchaseOrderRepository)
        {
            _purchaseOrderRepository = purchaseOrderRepository;
        }

        public async Task<DomainResult<PurchaseOrder>> Handle(GetOnePurchaseOrderQuery request, CancellationToken cancellationToken)
        {
            var purchaseOrder = await _purchaseOrderRepository.GetByIdAsync(request.Id);
            if (purchaseOrder == null)
            {
                return DomainResult<PurchaseOrder>.Error(ErrorType.NotFound, PurchaseOrderErrorCodes.NotFound, "Purchase order was not found");
            }
            return DomainResult<PurchaseOrder>.Success(purchaseOrder);
        }
    }
}
