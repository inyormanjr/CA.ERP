using CA.ERP.Domain.Core;
using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Domain.SupplierAgg;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Application.CommandQuery.SupplierCommandQuery.GetManySupplier
{
    public class GetManySupplierQuery : IRequest<PaginatedList<Supplier>>
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
