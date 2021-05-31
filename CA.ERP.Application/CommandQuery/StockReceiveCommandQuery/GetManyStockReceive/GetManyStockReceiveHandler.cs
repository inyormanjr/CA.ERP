using AutoMapper;
using CA.ERP.Domain.StockReceiveAgg;
using CA.ERP.Shared.Dto;
using CA.ERP.Shared.Dto.StockReceive;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Application.CommandQuery.StockReceiveCommandQuery.GetManyStockReceive
{
    public class GetManyStockReceiveHandler : IRequestHandler<GetManyStockReceiveQuery, PaginatedResponse<StockReceiveView>>
    {
        private readonly IStockReceiveRepository _stockReceiveRepository;
        private readonly IMapper _mapper;

        public GetManyStockReceiveHandler(IStockReceiveRepository stockReceiveRepository, IMapper mapper)
        {
            _stockReceiveRepository = stockReceiveRepository;
            _mapper = mapper;
        }
        public async Task<PaginatedResponse<StockReceiveView>> Handle(GetManyStockReceiveQuery request, CancellationToken cancellationToken)
        {
            List<StockReceive> stockReceives = await _stockReceiveRepository.GetManyStockReceiveAsync(request.Branch, request.DateCreated, request.DateReceived, request.Source, request.Stage, request.Skip, request.Take, cancellationToken);
            int totalCount = await _stockReceiveRepository.GetManyStockReceiveCountAsync(request.Branch, request.DateCreated, request.DateReceived, request.Source, request.Stage, cancellationToken);

            var list = _mapper.Map<List<StockReceiveView>>(stockReceives);

            return new PaginatedResponse<StockReceiveView>() {
                TotalCount = totalCount,
                Data = list
            };
        }
    }
}
