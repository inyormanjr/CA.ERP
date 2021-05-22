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
        public int Skip { get; set; } = 0;

        public int Take { get; set; } = 10;

        public string Model { get; set; }

        public Status Status { get; set; } = Status.Active;
    }
}
