using CA.ERP.Domain.Core.DomainResullts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Application.CommandQuery.StockReceiveCommandQuery.GenerateStockReceiveFromStockTransfer
{
    public class GenerateStockReceiveFromStockTransferCommand : IRequest<DomainResult<Guid>>
    {
        public Guid StockTransferId { get; set; }

        public GenerateStockReceiveFromStockTransferCommand()
        {

        }

        public GenerateStockReceiveFromStockTransferCommand(Guid stockTransferId)
        {
            StockTransferId = stockTransferId;
        }
    }
}
