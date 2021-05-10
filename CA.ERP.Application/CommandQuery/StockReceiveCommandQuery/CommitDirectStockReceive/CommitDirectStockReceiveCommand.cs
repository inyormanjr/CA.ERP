using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Shared.Dto.StockReceive;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Application.CommandQuery.StockReceiveCommandQuery.CommitDirectStockReceive
{
    public class CommitDirectStockReceiveCommand : IRequest<DomainResult<Guid>>
    {
        public StockReceiveCreate StockReceive { get; set; }

        public CommitDirectStockReceiveCommand(StockReceiveCreate stockReceive)
        {
            StockReceive = stockReceive;
        }
    }
}
