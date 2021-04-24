using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Shared.Dto.StockReceive;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Application.CommandQuery.StockReceiveCommandQuery.CommitStockReceive
{
    public class CommitStockReceiveCommand : IRequest<DomainResult>
    {
        public Guid Id { get; set; }
        public StockReceiveCommit StockReceive { get; set; }
        public CommitStockReceiveCommand(Guid id, StockReceiveCommit stockReceive)
        {
            Id = id;
            StockReceive = stockReceive;
        }
    }
}
