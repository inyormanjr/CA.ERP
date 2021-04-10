using CA.ERP.Domain.Core;
using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Domain.MasterProductAgg;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Application.CommandQuery.MasterProductCommandQuery.GetManyMasterProduct
{
    public class GetManyMasterProductHandler : IRequestHandler<GetManyMasterProductQuery, PaginatedList<MasterProduct>>
    {
        private readonly IMasterProductRepository _masterProductRepository;

        public GetManyMasterProductHandler(IMasterProductRepository masterProductRepository)
        {
            _masterProductRepository = masterProductRepository;
        }


        public async Task<PaginatedList<MasterProduct>> Handle(GetManyMasterProductQuery request, CancellationToken cancellationToken)
        {
            var masterProducts = await _masterProductRepository.GetManyAsync(request.Skip, request.Take, request.Status, cancellationToken: cancellationToken);
            var count = await _masterProductRepository.GetCountAsync(request.Status, cancellationToken: cancellationToken);

            return new PaginatedList<MasterProduct>(masterProducts, count);
        }
    }
}
