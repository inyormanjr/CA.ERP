using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Domain.PurchaseOrderAgg;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Application.CommandQuery.PurchaseOrderCommandQuery.GetOnePurchaseOrder
{
    public class GetOnePurchaseOrderQuery : IRequest<DomainResult<PurchaseOrder>>
    {
        public Guid Id { get; set; }
        public GetOnePurchaseOrderQuery(Guid id)
        {
            Id = id;
        }
    }
}
