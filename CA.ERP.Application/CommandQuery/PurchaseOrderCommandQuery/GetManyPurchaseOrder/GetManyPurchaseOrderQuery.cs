using CA.ERP.Domain.Core;
using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Domain.PurchaseOrderAgg;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Application.CommandQuery.PurchaseOrderCommandQuery.GetManyPurchaseOrder
{
    public class GetManyPurchaseOrderQuery : IRequest<DomainResult<PaginatedList<PurchaseOrder>>>
    {
        public int Skip { get; private set; }
        public int Take { get; private set; }
        public Status Status { get; private  set; }
        public string Barcode { get; set; }
        public DateTimeOffset? StartDate { get; internal set; }
        public DateTimeOffset? EndDate { get; internal set; }

        public GetManyPurchaseOrderQuery(int skip = 0, int take = int.MaxValue, Status status = Status.Active, string barcode = "", DateTimeOffset? startDate = null, DateTimeOffset? endDate = null)
        {
            Skip = skip;
            Take = take;
            Status = status;
            Barcode = barcode;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
