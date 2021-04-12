using CA.ERP.Domain.BrandAgg;
using CA.ERP.Domain.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Application.CommandQuery.BrandCommandQuery.GetManyBrand
{
    public class GetManyBrandHandler : IRequestHandler<GetManyBrandQuery, PaginatedList<Brand>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBrandRepository _brandRepository;

        public GetManyBrandHandler(IUnitOfWork unitOfWork, IBrandRepository brandRepository)
        {
            _unitOfWork = unitOfWork;
            _brandRepository = brandRepository;
        }

        public async Task<PaginatedList<Brand>> Handle(GetManyBrandQuery request, CancellationToken cancellationToken)
        {
            var brands = await _brandRepository.GetManyAsync(request.Skip, request.Take, request.Status, cancellationToken: cancellationToken);
            var count = await _brandRepository.GetCountAsync(request.Status, cancellationToken: cancellationToken);

            return new PaginatedList<Brand>(brands, count);
        }
    }
}
