using CA.ERP.Shared.Dto;
using CA.ERP.Shared.Dto.StockTransfer;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Application.CommandQuery.StockTransferCommandQuery.GetManyStockTransfer
{
    public class GetManyStockTransferQuery: IRequest<PaginatedResponse<StockTransferView>>
    {
        public int Skip { get; set; } = 0;

        public int Take { get; set; } = 10;
    }
}
