using CA.ERP.Shared.Dto;
using CA.ERP.Shared.Dto.StockReceive;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Application.CommandQuery.StockReceiveCommandQuery.GetManyStockReceive
{
    public class GetManyStockReceiveQuery : IRequest<PaginatedResponse<StockReceiveView>>
    {
        public Guid? Branch { get; set; }
        public Guid? SupplierId { get; set; }
        public DateTimeOffset? DateReceived { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }

        public GetManyStockReceiveQuery(Guid? branch, Guid? supplierId, DateTimeOffset? dateReceived, int skip, int take)
        {
            Branch = branch;
            SupplierId = supplierId;
            DateReceived = dateReceived;
            Skip = skip;
            Take = take;
        }
    }
}
