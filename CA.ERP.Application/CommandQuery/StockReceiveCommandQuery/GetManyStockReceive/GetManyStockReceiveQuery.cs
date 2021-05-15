using CA.ERP.Common.Types;
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
        public DateTimeOffset? DateCreated { get; set; }
        public StockSource? Source { get; set; }
        public StockReceiveStage? Stage { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }

        public GetManyStockReceiveQuery(Guid? branch, Guid? supplierId, DateTimeOffset? dateCreated, DateTimeOffset? dateReceived, StockSource? source, StockReceiveStage? stage, int skip, int take)
        {
            Branch = branch;
            SupplierId = supplierId;
            DateCreated = dateCreated;
            DateReceived = dateReceived;
            Skip = skip;
            Take = take;
            Source = source;
            Stage = stage;
        }
    }
}
