using CA.ERP.Domain.Core;
using CA.ERP.Domain.MasterProductAgg;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Application.CommandQuery.MasterProductCommandQuery.GetManyMasterProductByBrandAndSupplier
{
    public class GetManyMasterProductByBrandAndSupplierHandler : IRequestHandler<GetManyMasterProductByBrandAndSupplierQuery, List<MasterProduct>>
    {
        private readonly IMasterProductRepository _masterProductRepository;

        public GetManyMasterProductByBrandAndSupplierHandler(IMasterProductRepository masterProductRepository)
        {
            _masterProductRepository = masterProductRepository;
        }


        public Task<List<MasterProduct>> Handle(GetManyMasterProductByBrandAndSupplierQuery request, CancellationToken cancellationToken)
        {
            return _masterProductRepository.GetManyWithBrandAndSupplierAsync(request.BrandId, request.SupplierId, cancellationToken);
        }
    }
}
