using AutoMapper;
using CA.ERP.Domain.Core;
using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Domain.PurchaseOrderAgg;
using CA.ERP.Shared.Dto;
using CA.ERP.Shared.Dto.PurchaseOrder;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Application.CommandQuery.PurchaseOrderCommandQuery.GetManyPurchaseOrder
{
    public class GetManyPurchaseOrderHandler : IRequestHandler<GetManyPurchaseOrderQuery, DomainResult<PaginatedResponse<PurchaseOrderView>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;
        private readonly IMapper _mapper;

        public GetManyPurchaseOrderHandler(IUnitOfWork unitOfWork, IPurchaseOrderRepository purchaseOrderRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _purchaseOrderRepository = purchaseOrderRepository;
            _mapper = mapper;
        }

        public async Task<DomainResult<PaginatedResponse<PurchaseOrderView>>> Handle(GetManyPurchaseOrderQuery request, CancellationToken cancellationToken)
        {

            int count = await _purchaseOrderRepository.CountAsync(request.Barcode, request.StartDate, request.EndDate, cancellationToken);
            IEnumerable<PurchaseOrder> purchaseOrders = await _purchaseOrderRepository.GetManyAsync(request.Barcode, request.StartDate, request.EndDate, request.Skip, request.Take, cancellationToken);

            var list = new PaginatedResponse<PurchaseOrderView>() {
                Data = _mapper.Map<List<PurchaseOrderView>>(purchaseOrders),
                TotalCount = count
            };
            return DomainResult<PaginatedResponse<PurchaseOrderView>>.Success(list);
        }
    }
}
