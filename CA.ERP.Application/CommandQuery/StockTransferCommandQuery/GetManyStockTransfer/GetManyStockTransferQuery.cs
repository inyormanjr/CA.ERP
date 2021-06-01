using CA.ERP.Common.Types;
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
        public string Number { get; set; }

        public StockTransferStatus? StockTransferStatus { get; set; }

        public int Skip { get; set; } = 0;

        public int Take { get; set; } = 10;
    }
}
