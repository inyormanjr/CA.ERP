using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Shared.Dto.StockReceive;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Application.CommandQuery.StockReceiveCommandQuery.CreateDirectStockReceive
{
    public class CreateDirectStockReceiveCommand : IRequest<DomainResult<Guid>>
    {
        public StockReceiveCreate StockReceive { get; set; }

        public CreateDirectStockReceiveCommand(StockReceiveCreate stockReceive)
        {
            StockReceive = stockReceive;
        }
    }
}
