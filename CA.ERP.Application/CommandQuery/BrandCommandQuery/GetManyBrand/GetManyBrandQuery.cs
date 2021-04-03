using CA.ERP.Domain.BrandAgg;
using CA.ERP.Domain.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Application.CommandQuery.BrandCommandQuery.GetManyBrand
{
    public class GetManyBrandQuery : IRequest<PaginatedList<Brand>>
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public Status Status { get; set; }
        public GetManyBrandQuery(int skip = 0, int take = int.MaxValue, Status status = Status.Active)
        {
            Skip = skip;
            Take = take;
            Status = status;
        }
    }
}
