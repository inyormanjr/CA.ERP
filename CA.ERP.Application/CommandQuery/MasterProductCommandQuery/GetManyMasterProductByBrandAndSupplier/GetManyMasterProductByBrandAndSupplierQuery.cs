using CA.ERP.Domain.Core;
using CA.ERP.Domain.MasterProductAgg;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Application.CommandQuery.MasterProductCommandQuery.GetManyMasterProductByBrandAndSupplier
{
    public class GetManyMasterProductByBrandAndSupplierQuery : IRequest<List<MasterProduct>>
    {
        public Guid BrandId { get; set; }
        public Guid SupplierId { get; set; }

        public GetManyMasterProductByBrandAndSupplierQuery(Guid brandId, Guid supplierId)
        {
            BrandId = brandId;
            SupplierId = supplierId;
        }

    }
}
