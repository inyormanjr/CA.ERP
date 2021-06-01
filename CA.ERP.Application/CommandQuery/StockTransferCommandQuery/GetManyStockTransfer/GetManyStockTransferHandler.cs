using AutoMapper;
using CA.ERP.Domain.StockTransferAgg;
using CA.ERP.Shared.Dto;
using CA.ERP.Shared.Dto.StockTransfer;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Application.CommandQuery.StockTransferCommandQuery.GetManyStockTransfer
{
    public class GetManyStockTransferHandler : IRequestHandler<GetManyStockTransferQuery, PaginatedResponse<StockTransferView>>
    {
        private readonly IMapper _mapper;
        private readonly IStockTransferRepository _stockTransferRepository;

        public GetManyStockTransferHandler(IMapper mapper, IStockTransferRepository stockTransferRepository)
        {
            _mapper = mapper;
            _stockTransferRepository = stockTransferRepository;
        }

        public async Task<PaginatedResponse<StockTransferView>> Handle(GetManyStockTransferQuery request, CancellationToken cancellationToken)
        {
            var data = await _stockTransferRepository.GetManyAsync(request.Number, request.StockTransferStatus, request.Skip, request.Take, Common.Types.Status.All);
            var count = await _stockTransferRepository.GetCountAsync(request.Number, request.StockTransferStatus, Common.Types.Status.All);

            return new PaginatedResponse<StockTransferView>(_mapper.Map<List<StockTransferView>>(data), count);
        }
    }
}
