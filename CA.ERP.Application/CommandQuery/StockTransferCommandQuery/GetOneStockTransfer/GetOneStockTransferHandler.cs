using AutoMapper;
using CA.ERP.Common.ErrorCodes;
using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Domain.StockTransferAgg;
using CA.ERP.Shared.Dto.StockTransfer;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Application.CommandQuery.StockTransferCommandQuery.GetOneStockTransfer
{
    public class GetOneStockTransferHandler : IRequestHandler<GetOneStockTransferQuery, DomainResult<StockTransferView>>
    {
        private readonly IMapper _mapper;
        private readonly IStockTransferRepository _stockTransferRepository;

        public GetOneStockTransferHandler(IMapper mapper, IStockTransferRepository stockTransferRepository)
        {
            _mapper = mapper;
            _stockTransferRepository = stockTransferRepository;
        }

        public async Task<DomainResult<StockTransferView>> Handle(GetOneStockTransferQuery request, CancellationToken cancellationToken)
        {
            var stockTransfer = await _stockTransferRepository.GetByIdAsync(request.Id, cancellationToken);
            if (stockTransfer == null)
            {
                return DomainResult<StockTransferView>.Error(ErrorType.NotFound, StockTransferErrorCodes.NotFound, "Stock transfer was not found");
            }

            return _mapper.Map<StockTransferView>(stockTransfer);
        }
    }
}
