using CA.ERP.Domain.BrandAgg;
using CA.ERP.Domain.Core;
using CA.ERP.Domain.Core.DomainResullts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Application.CommandQuery.BrandCommandQuery.CreateBrand
{
    public class CreateBrandHandler : IRequestHandler<CreateBrandCommand, DomainResult<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBrandRepository _brandRepository;

        public CreateBrandHandler(IUnitOfWork unitOfWork, IBrandRepository brandRepository)
        {
            _unitOfWork = unitOfWork;
            _brandRepository = brandRepository;
        }
        public async Task<DomainResult<Guid>> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            var result = Brand.Create(request.Name, request.Description);
            if (!result.IsSuccess)
            {
                return result.ConvertTo<Guid>();
            }

            var brand = result.Result;
            var id = await _brandRepository.AddAsync(brand, cancellationToken: cancellationToken);

            await _unitOfWork.CommitAsync();
            return DomainResult<Guid>.Success(id);
        }
    }
}
