using CA.ERP.Common.Types;
using CA.ERP.Domain.Core;
using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Domain.MasterProductAgg;
using CA.ERP.Shared.Dto;
using CA.ERP.Shared.Dto.MasterProduct;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Application.CommandQuery.MasterProductCommandQuery.GetManyMasterProduct
{
    public class GetManyMasterProductQuery : IRequest<PaginatedResponse<MasterProductView>>
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public Status Status { get; set; }
        public GetManyMasterProductQuery(int skip = 0, int take = int.MaxValue, Status status = Status.Active)
        {
            Skip = skip;
            Take = take;
            Status = status;
        }
    }
}
