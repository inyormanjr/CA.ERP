using CA.ERP.Domain.Core;
using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Domain.SupplierAgg;
using CA.ERP.Shared.Dto;
using CA.ERP.Shared.Dto.Supplier;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Application.CommandQuery.SupplierCommandQuery.GetManySupplier
{
    public class GetManySupplierQuery : IRequest<PaginatedResponse<SupplierView>>
    {
        public int Skip { get; private set; }
        public int Take { get; private set; }
        public string Name { get; set; }

        public GetManySupplierQuery(string name, int skip, int take)
        {
            Name = name;
            Skip = skip;
            Take = take;
        }
    }
}
