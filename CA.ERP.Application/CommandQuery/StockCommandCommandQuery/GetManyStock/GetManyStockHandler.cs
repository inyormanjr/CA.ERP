using AutoMapper;
using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Domain.StockAgg;
using CA.ERP.Shared.Dto;
using CA.ERP.Shared.Dto.Stock;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Application.CommandQuery.StockCommandCommandQuery.GetManyStock
{
    public class GetManyStockHandler : IRequestHandler<GetManyStockQuery, DomainResult<PaginatedResponse<StockView>>>
    {
        private readonly IMapper _mapper;
        private readonly IStockRepository _stockRepository;

        public GetManyStockHandler(IMapper mapper, IStockRepository stockRepository)
        {
            _mapper = mapper;
            _stockRepository = stockRepository;
        }

        public async Task<DomainResult<PaginatedResponse<StockView>>> Handle(GetManyStockQuery request, CancellationToken cancellationToken)
        {
            var count = await _stockRepository.CountAsync(request.BrandId, request.MasterProductId, request.StockNumber, request.SerialNumber, request.StockStatus, cancellationToken);
            var stocks = await _stockRepository.GetManyAsync(request.BrandId, request.MasterProductId, request.StockNumber, request.SerialNumber, request.StockStatus, request.Skip, request.Take, cancellationToken);

            var list = new PaginatedResponse<StockView>()
            {
                Data = _mapper.Map<List<StockView>>(stocks),
                TotalCount = count
            };
            return DomainResult<PaginatedResponse<StockView>>.Success(list);
        }
    }
}
