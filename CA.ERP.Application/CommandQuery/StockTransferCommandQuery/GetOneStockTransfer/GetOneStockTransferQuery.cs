using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Shared.Dto.StockTransfer;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Application.CommandQuery.StockTransferCommandQuery.GetOneStockTransfer
{
    public class GetOneStockTransferQuery : IRequest<DomainResult<StockTransferView>>
    {
        public Guid Id { get; set; }

        public GetOneStockTransferQuery(Guid id)
        {
            Id = id;
        }
    }
}
