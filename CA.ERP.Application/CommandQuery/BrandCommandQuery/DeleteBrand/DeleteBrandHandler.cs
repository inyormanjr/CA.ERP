using CA.ERP.Domain.BrandAgg;
using CA.ERP.Domain.Core;
using CA.ERP.Domain.Core.DomainResullts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Application.CommandQuery.BrandCommandQuery.DeleteBrand
{
    public class DeleteBrandHandler : IRequestHandler<DeleteBrandCommand, DomainResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBrandRepository _brandRepository;

        public DeleteBrandHandler(IUnitOfWork unitOfWork, IBrandRepository brandRepository)
        {
            _unitOfWork = unitOfWork;
            _brandRepository = brandRepository;
        }
        public async Task<DomainResult> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            await _brandRepository.DeleteAsync(request.Id, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            return DomainResult.Success();
        }
    }
}
