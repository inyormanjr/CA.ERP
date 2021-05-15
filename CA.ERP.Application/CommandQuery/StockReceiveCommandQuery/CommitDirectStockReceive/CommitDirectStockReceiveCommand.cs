using CA.ERP.Domain.Core.DomainResullts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Application.CommandQuery.StockReceiveCommandQuery.CommitDirectStockReceive
{
    public class CommitDirectStockReceiveCommand : IRequest<DomainResult>
    {
        public Guid Id { get; set; }

        public CommitDirectStockReceiveCommand(Guid id)
        {
            Id = id;
        }
    }
}
