using CA.ERP.Domain.Core;
using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Domain.MasterProductAgg;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Application.CommandQuery.MasterProductCommandQuery.UpdateMasterProduct
{
    public class UpdateMasterProductHandler : IRequestHandler<UpdateMasterProductCommand, DomainResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMasterProductRepository _masterProductRepository;


        public UpdateMasterProductHandler(IUnitOfWork unitOfWork, IMasterProductRepository masterProductRepository)
        {

            _unitOfWork = unitOfWork;
            _masterProductRepository = masterProductRepository;

        }


        public async Task<DomainResult> Handle(UpdateMasterProductCommand request, CancellationToken cancellationToken)
        {
            var masterProduct = await _masterProductRepository.GetByIdAsync(request.Id, cancellationToken);
            if (masterProduct == null)
            {
                return DomainResult.Error(MasterProductErrorCodes.MasterProductNotFound, "Master product was not found while updating");
            }

            masterProduct.Update(request.Model, request.Description, request.BrandId, request.ProductStatus);

            await _masterProductRepository.UpdateAsync(request.Id, masterProduct);
            return DomainResult.Success();
        }
    }
}
