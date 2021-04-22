using AutoMapper;
using CA.ERP.Common.ErrorCodes;
using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Domain.IdentityAgg;
using CA.ERP.Domain.StockReceiveAgg;
using CA.ERP.Shared.Dto.Stock;
using CA.ERP.Shared.Dto.StockReceive;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Application.CommandQuery.StockReceiveCommandQuery.GetOneStockReceive
{
    public class GetOneStockReceiveHandler : IRequestHandler<GetOneStockReceiveQuery, DomainResult<StockReceiveView>>
    {
        private readonly IStockReceiveRepository _stockReceiveRepository;
        private readonly IIdentityProvider _identityProvider;
        private readonly IMapper _mapper;

        public GetOneStockReceiveHandler(IStockReceiveRepository stockReceiveRepository, IIdentityProvider identityProvider, IMapper mapper)
        {
            _stockReceiveRepository = stockReceiveRepository;
            _identityProvider = identityProvider;
            _mapper = mapper;
        }
        public async Task<DomainResult<StockReceiveView>> Handle(GetOneStockReceiveQuery request, CancellationToken cancellationToken)
        {
            var stockReceive = await _stockReceiveRepository.GetByIdWithItemsAsync(request.Id, cancellationToken);
            if (stockReceive == null)
            {
                return DomainResult<StockReceiveView>.Error(ErrorType.NotFound, StockReceiveErrorCodes.NotFound, "Stock Receive not found");
            }
            var identity = await _identityProvider.GetCurrentIdentity();
            if (!identity.BelongsToBranch(stockReceive.BranchId))
            {
                return DomainResult<StockReceiveView>.Error(ErrorType.Forbidden, IdentityErrorCodes.Forbidden, "Your are not assigned to this branch.");
            }

            var stockReceiveView = _mapper.Map<StockReceiveView>(stockReceive);
            return DomainResult<StockReceiveView>.Success(stockReceiveView);
        }
    }
}
