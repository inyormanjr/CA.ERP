using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Domain.PurchaseOrderAgg;
using CA.ERP.Domain.Services;
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
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;
        private readonly IStockReceiveGeneratorService _stockReceiveGeneratorService;
        private readonly IStockReceiveRepository _stockReceiveRepository;

        public GenerateStockReceiveFromPurchaseOrderHandler(IPurchaseOrderRepository purchaseOrderRepository, IStockReceiveGeneratorService stockReceiveGeneratorService, IStockReceiveRepository stockReceiveRepository)
        {
            _purchaseOrderRepository = purchaseOrderRepository;
            _stockReceiveGeneratorService = stockReceiveGeneratorService;
            _stockReceiveRepository = stockReceiveRepository;
        }
        public async Task<DomainResult<Guid>> Handle(GenerateStockReceiveFromPurchaseOrderCommand request, CancellationToken cancellationToken)
        {
            var purchaseOrder = await _purchaseOrderRepository.GetByIdAsync(request.PurchaseOrderId);
            var createStockReceiveResult =  _stockReceiveGeneratorService.FromPurchaseOrder(purchaseOrder);
            if (!createStockReceiveResult.IsSuccess)
            {
                return createStockReceiveResult.ConvertTo<Guid>();
            }
            var id = await _stockReceiveRepository.AddAsync(createStockReceiveResult.Result, cancellationToken);
            return DomainResult<Guid>.Success(id);

        }
    }
}
