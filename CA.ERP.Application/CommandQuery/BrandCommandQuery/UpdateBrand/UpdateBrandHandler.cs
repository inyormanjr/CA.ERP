using CA.ERP.Domain.BrandAgg;
using CA.ERP.Domain.Core;
using CA.ERP.Domain.Core.DomainResullts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Application.CommandQuery.BrandCommandQuery.UpdateBrand
{
    public class UpdateBrandHandler : IRequestHandler<UpdateBrandCommand, DomainResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBrandRepository _brandRepository;

        public UpdateBrandHandler(IUnitOfWork unitOfWork, IBrandRepository brandRepository)
        {
            _unitOfWork = unitOfWork;
            _brandRepository = brandRepository;
        }

        public async Task<DomainResult> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            Brand brand = await _brandRepository.GetByIdAsync(request.Id, cancellationToken);
            if (brand == null)
            {
                return DomainResult.Error(ErrorType.NotFound, BrandErrorCodes.InvalidName, "Brand not found");
            }
            var updateResult = brand.Update(request.Name, request.Description);
            if (!updateResult.IsSuccess)
            {
                return updateResult;
            }
            await _brandRepository.UpdateAsync(request.Id, brand, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            return DomainResult.Success();
        }
    }
}
