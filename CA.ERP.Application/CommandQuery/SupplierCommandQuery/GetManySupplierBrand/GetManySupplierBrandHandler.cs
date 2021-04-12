using CA.ERP.Domain.SupplierAgg;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Application.CommandQuery.SupplierCommandQuery.GetManySupplierBrand
{
    public class GetManySupplierBrandHandler : IRequestHandler<GetManySupplierBrandQuery, List<SupplierBrand>>
    {
        private readonly ISupplierRepository _supplierRepository;

        public GetManySupplierBrandHandler(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }
        public Task<List<SupplierBrand>> Handle(GetManySupplierBrandQuery request, CancellationToken cancellationToken)
        {
           return _supplierRepository.GetManySupplierBrandAsync(request.SupplierId, cancellationToken);
        }
    }
}
