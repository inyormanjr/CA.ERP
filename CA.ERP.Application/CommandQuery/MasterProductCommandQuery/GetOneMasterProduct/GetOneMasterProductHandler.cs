using CA.ERP.Domain.Core;
using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Domain.MasterProductAgg;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Application.CommandQuery.MasterProductCommandQuery.GetOneMasterProduct
{
    public class GetOneMasterProductHandler : IRequestHandler<GetOneMasterProductQuery, DomainResult<MasterProduct>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMasterProductRepository _masterProductRepository;


        public GetOneMasterProductHandler(IUnitOfWork unitOfWork, IMasterProductRepository masterProductRepository)
        {

            _unitOfWork = unitOfWork;
            _masterProductRepository = masterProductRepository;

        }
        public async Task<DomainResult<MasterProduct>> Handle(GetOneMasterProductQuery request, CancellationToken cancellationToken)
        {
            var masterProduct = await _masterProductRepository.GetByIdAsync(request.Id, cancellationToken);

            if (masterProduct == null)
            {
                return DomainResult<MasterProduct>.Error(MasterProductErrorCodes.MasterProductNotFound, "Master product was not found");
            }

            return DomainResult<MasterProduct>.Success(masterProduct);
        }
    }
}
