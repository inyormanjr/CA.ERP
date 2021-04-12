using CA.ERP.Domain.Core;
using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Domain.PurchaseOrderAgg;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Application.CommandQuery.PurchaseOrderCommandQuery.GetManyPurchaseOrder
{
    public class GetManyPurchaseOrderHandler : IRequestHandler<GetManyPurchaseOrderQuery, DomainResult<PaginatedList<PurchaseOrder>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;

        public GetManyPurchaseOrderHandler(IUnitOfWork unitOfWork, IPurchaseOrderRepository purchaseOrderRepository)
        {
            _unitOfWork = unitOfWork;
            _purchaseOrderRepository = purchaseOrderRepository;
        }

        public async Task<DomainResult<PaginatedList<PurchaseOrder>>> Handle(GetManyPurchaseOrderQuery request, CancellationToken cancellationToken)
        {

            int count = await _purchaseOrderRepository.CountAsync(request.Barcode, request.StartDate, request.EndDate, cancellationToken);
            IEnumerable<PurchaseOrder> purchaseOrders = await _purchaseOrderRepository.GetManyAsync(request.Barcode, request.StartDate, request.EndDate, request.Skip, request.Take, cancellationToken);

            var list = new PaginatedList<PurchaseOrder>(purchaseOrders.ToList(), count);
            return DomainResult<PaginatedList<PurchaseOrder>>.Success(list);
        }
    }
}
