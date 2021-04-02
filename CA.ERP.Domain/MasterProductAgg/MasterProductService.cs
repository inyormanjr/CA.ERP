using CA.ERP.Domain.Base;
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

        public async Task<Guid> CreateMasterProduct(string model, string description,  Guid brandId, CancellationToken cancellationToken = default)
        {

            MasterProduct masterProduct = MasterProduct.Create(model, description,  brandId);
            masterProduct.CreatedBy = _userHelper.GetCurrentUserId();
            masterProduct.UpdatedBy = _userHelper.GetCurrentUserId();
            Guid id = await _masterProductRepository.AddAsync(masterProduct, cancellationToken);
            await _unitOfWork.CommitAsync();
            return id;
        }

        public async Task UpdateAsync(Guid id, string model, string description, Guid brandId, ProductStatus productStatus, CancellationToken cancellationToken)
        {
            var masterProduct = await _masterProductRepository.GetByIdAsync(id, cancellationToken);
            checkMasterProduct(masterProduct);
            masterProduct.Update(model, description, brandId, productStatus);

            await _masterProductRepository.UpdateAsync(id, masterProduct);
        }

        private static void checkMasterProduct(MasterProduct masterProduct)
        {
            if (masterProduct == null)
            {
                throw new MasterProductException(MasterProductException.MasterProductNotFound, "Master product was not found while updating");
            }
        }

        public async Task<MasterProduct> GetOneAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var masterProduct = await _masterProductRepository.GetByIdAsync(id, cancellationToken);
            checkMasterProduct(masterProduct);
            return masterProduct;
        }
    }
}
