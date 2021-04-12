using CA.ERP.Domain.Core;
using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Domain.MasterProductAgg;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Application.CommandQuery.MasterProductCommandQuery.DeleteMasterProduct
{
    public class DeleteMasterProductHandler : IRequestHandler<DeleteMasterProductCommand, DomainResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMasterProductRepository _masterProductRepository;


        public DeleteMasterProductHandler(IUnitOfWork unitOfWork, IMasterProductRepository masterProductRepository)
        {

            _unitOfWork = unitOfWork;
            _masterProductRepository = masterProductRepository;

        }

        public async Task<DomainResult> Handle(DeleteMasterProductCommand request, CancellationToken cancellationToken)
        {
            await _masterProductRepository.DeleteAsync(request.Id, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            return DomainResult.Success();
        }
    }
}
