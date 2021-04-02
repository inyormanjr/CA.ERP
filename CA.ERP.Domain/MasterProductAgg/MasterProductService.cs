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
        private readonly IValidator<MasterProduct> _masterProductValidator;

        public MasterProductService(IUnitOfWork unitOfWork, IMasterProductRepository masterProductRepository, IUserHelper userHelper)
            : base(unitOfWork, masterProductRepository, null, userHelper)
        {
            _masterProductRepository = masterProductRepository;
        }

        public async Task<DomainResult<Guid>> CreateMasterProduct(string model, string description,  Guid brandId, CancellationToken cancellationToken = default)
        {

            var result = MasterProduct.Create(model, description, brandId);
            if (result.IsSuccess)
            {
                MasterProduct masterProduct = result.Result;
                masterProduct.CreatedBy = _userHelper.GetCurrentUserId();
                masterProduct.UpdatedBy = _userHelper.GetCurrentUserId();
                Guid id = await _masterProductRepository.AddAsync(masterProduct, cancellationToken);
                await _unitOfWork.CommitAsync();
                return DomainResult<Guid>.Success(id);
            }
            else
            {
                return result.ConvertTo<Guid>();
            }

          
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
