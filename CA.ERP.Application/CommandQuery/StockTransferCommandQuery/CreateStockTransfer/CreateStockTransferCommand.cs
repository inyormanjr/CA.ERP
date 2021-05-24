using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Shared.Dto.StockTransfer;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Application.CommandQuery.StockTransferCommandQuery.CreateStockTransfer
{
    public class CreateStockTransferCommand : IRequest<DomainResult<Guid>>
    {
        public StockTransferCreate StockTransfer { get; set; }

        public CreateStockTransferCommand(StockTransferCreate stockTransfer)
        {
            StockTransfer = stockTransfer;
        }

    }
}
