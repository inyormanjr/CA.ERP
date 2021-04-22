using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Shared.Dto.Stock;
using CA.ERP.Shared.Dto.StockReceive;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Application.CommandQuery.StockReceiveCommandQuery.GetOneStockReceive
{
    public class GetOneStockReceiveQuery : IRequest<DomainResult<StockReceiveView>>
    {
        public Guid Id { get; set; }
        public GetOneStockReceiveQuery(Guid id)
        {
            Id = id;
        }
    }
}
