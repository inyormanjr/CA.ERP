using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Domain.PurchaseOrderAgg;
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
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;

        public GenerateStockReceiveFromPurchaseOrderHandler(IPurchaseOrderRepository purchaseOrderRepository)
        {
            _purchaseOrderRepository = purchaseOrderRepository;
        }
        public Task<DomainResult<Guid>> Handle(GenerateStockReceiveFromPurchaseOrderCommand request, CancellationToken cancellationToken)
        {
            var purchaseOrder = _purchaseOrderRepository
        }
    }
}
