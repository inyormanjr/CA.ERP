using CA.ERP.Domain.Base;
using CA.ERP.Domain.Core.DomainResullts;
using CA.ERP.Domain.UnitOfWorkAgg;
using CA.ERP.Domain.UserAgg;
using FluentValidation;
using FluentValidation.Results;
using OneOf;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Domain.MasterProductAgg
{
    public class MasterProductService : ServiceBase<MasterProduct>
    {
        private readonly IMasterProductRepository _masterProductRepository;


        public MasterProductService(IUnitOfWork unitOfWork, IMasterProductRepository masterProductRepository, IUserHelper userHelper)
            : base(unitOfWork, masterProductRepository, null, userHelper)
        {
            _masterProductRepository = masterProductRepository;
        }


        public async Task<DomainResult> UpdateAsync(Guid id, string model, string description, Guid brandId, ProductStatus productStatus, CancellationToken cancellationToken)
        {
            var masterProduct = await _masterProductRepository.GetByIdAsync(id, cancellationToken);
            if (masterProduct == null)
            {
                return DomainResult.Error(MasterProductErrorCodes.MasterProductNotFound, "Master product was not found while updating");
            }

            masterProduct.Update(model, description, brandId, productStatus);

            await _masterProductRepository.UpdateAsync(id, masterProduct);
            return DomainResult.Success();
        }


        public async  Task<DomainResult<MasterProduct>> GetOneAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var  masterProduct = await _masterProductRepository.GetByIdAsync(id, cancellationToken);

            if (masterProduct == null)
            {
                return DomainResult<MasterProduct>.Error(MasterProductErrorCodes.MasterProductNotFound, "Master product was not found");
            }

            return DomainResult<MasterProduct>.Success(masterProduct);

        }
    }
}
