using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Shared.Dto.StockReceive;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Application.CommandQuery.StockReceiveCommandQuery.GenerateStockReceiveFromPurchaseOrder
{
    public class GenerateStockReceiveFromPurchaseOrderCommand : IRequest<DomainResult<Guid>>
    {
        public Guid PurchaseOrderId { get; set; }
    }
}
