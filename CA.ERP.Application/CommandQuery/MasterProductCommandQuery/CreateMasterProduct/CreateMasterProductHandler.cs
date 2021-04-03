using CA.ERP.Domain.Core;
using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Domain.MasterProductAgg;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Application.CommandQuery.MasterProductCommandQuery.CreateMasterProduct
{
    public class CreateMasterProductHandler : IRequestHandler<CreateMasterProductCommand, DomainResult<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMasterProductRepository _masterProductRepository;


        public CreateMasterProductHandler(IUnitOfWork unitOfWork, IMasterProductRepository masterProductRepository)
        {

            _unitOfWork = unitOfWork;
            _masterProductRepository = masterProductRepository;

        }

        public async Task<DomainResult<Guid>> Handle(CreateMasterProductCommand request, CancellationToken cancellationToken)
        {
            var result = MasterProduct.Create(request.Model, request.Description, request.BrandId);
            if (result.IsSuccess)
            {
                MasterProduct masterProduct = result.Result;

                Guid id = await _masterProductRepository.AddAsync(masterProduct, cancellationToken);
                await _unitOfWork.CommitAsync();
                return DomainResult<Guid>.Success(id);
            }
            else
            {
                return result.ConvertTo<Guid>();
            }
        }
    }
}
