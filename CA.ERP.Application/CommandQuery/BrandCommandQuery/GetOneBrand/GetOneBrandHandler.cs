using CA.ERP.Domain.BrandAgg;
using CA.ERP.Domain.Core;
using CA.ERP.Domain.Core.DomainResullts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Application.CommandQuery.BrandCommandQuery.GetOneBrand
{
    public class GetOneBrandHandler : IRequestHandler<GetOneBrandQuery, DomainResult<Brand>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBrandRepository _brandRepository;

        public GetOneBrandHandler(IUnitOfWork unitOfWork, IBrandRepository brandRepository)
        {
            _unitOfWork = unitOfWork;
            _brandRepository = brandRepository;
        }

        public async Task<DomainResult<Brand>> Handle(GetOneBrandQuery request, CancellationToken cancellationToken)
        {
            Brand brand = await _brandRepository.GetByIdAsync(request.Id, cancellationToken);
            if (brand == null)
            {
                return DomainResult<Brand>.Error(ErrorType.NotFound, BrandErrorCodes.InvalidName, "Brand not found");
            }
            return DomainResult<Brand>.Success(brand);
        }
    }
}
