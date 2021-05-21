using CA.ERP.Domain.Core.DomainResullts;
using Dto = CA.ERP.Shared.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using CA.ERP.Shared.Dto;
using CA.ERP.Common.Types;

namespace CA.ERP.Application.CommandQuery.StockCommandCommandQuery.GetManyStock
{
    public class GetManyStockQuery : IRequest<DomainResult<PaginatedResponse<Dto.Stock.StockView>>>
    {
        public Guid? BranchId { get; set; }

        public Guid? BrandId { get; set; }

        public Guid? MasterProductId { get; set; }

        public string StockNumber { get; set; }

        public string SerialNumber { get; set; }

        public StockStatus? StockStatus { get; set; }

        public int Skip { get; set; } = 0;

        public int Take { get; set; } = 10;

    }
}
