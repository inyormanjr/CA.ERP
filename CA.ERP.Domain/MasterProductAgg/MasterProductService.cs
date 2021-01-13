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
    public class MasterProductService: ServiceBase<MasterProduct>
    {
        private readonly IMasterProductRepository _masterProductRepository;
        private readonly IMasterProductFactory _masterProductFactory;
        private readonly IValidator<MasterProduct> _masterProductValidator;

        public MasterProductService(IUnitOfWork unitOfWork,IMasterProductRepository masterProductRepository, IMasterProductFactory masterProductFactory, IValidator<MasterProduct> masterProductValidator, IUserHelper userHelper)
            : base(unitOfWork, masterProductRepository, masterProductValidator, userHelper)
        {
            _masterProductRepository = masterProductRepository;
            _masterProductFactory = masterProductFactory;
            _masterProductValidator = masterProductValidator;
        }

        public async Task<OneOf<Guid, List<ValidationFailure>>> CreateMasterProduct(string model,string description,ProductStatus productStatus, Guid brandId, CancellationToken cancellationToken = default)
        {
            OneOf<Guid, List<ValidationFailure>> ret;
            MasterProduct masterProduct = _masterProductFactory.CreateMasterProduct(model, description, productStatus, brandId);
            var validationResult = await _masterProductValidator.ValidateAsync(masterProduct, cancellationToken);
            if (!validationResult.IsValid)
            {
                ret = validationResult.Errors.ToList();
            }
            else
            {
                masterProduct.CreatedBy = _userHelper.GetCurrentUserId();
                masterProduct.UpdatedBy = _userHelper.GetCurrentUserId();
                ret = await _masterProductRepository.AddAsync(masterProduct, cancellationToken);
            }
            await _unitOfWork.CommitAsync();
            return ret;
        }

        
    }
}
