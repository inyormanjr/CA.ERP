using CA.ERP.Domain.SupplierAgg;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Application.CommandQuery.SupplierCommandQuery.GetManySupplierBrand
{
    public class GetManySupplierBrandQuery : IRequest<List<SupplierBrand>>
    {
        public Guid SupplierId { get; set; }

        public GetManySupplierBrandQuery(Guid supplierId)
        {
            SupplierId = supplierId;
        }
    }
}
