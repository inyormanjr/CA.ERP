using CA.ERP.Domain.Base;
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
    public class MasterProductService: ServiceBase
    {
        private readonly IMasterProductRepository _masterProductRepository;
        private readonly IMasterProductFactory _masterProductFactory;
        private readonly IValidator<MasterProduct> _masterProductValidator;

        public MasterProductService(IMasterProductRepository masterProductRepository, IMasterProductFactory masterProductFactory, IValidator<MasterProduct> masterProductValidator)
        {
            _masterProductRepository = masterProductRepository;
            _masterProductFactory = masterProductFactory;
            _masterProductValidator = masterProductValidator;
        }

        public async Task<OneOf<Guid, List<ValidationFailure>>> CreateMasterProduct(string model,string description,ProductStatus productStatus, Guid brandId, CancellationToken cancellationToken = default)
        {
            OneOf<Guid, List<ValidationFailure>> ret = Guid.Empty;
            MasterProduct masterProduct = _masterProductFactory.CreateMasterProduct(model, description, productStatus, brandId);
            var validationResult = _masterProductValidator.Validate(masterProduct);
            if (!validationResult.IsValid)
            {
                ret = validationResult.Errors.ToList();
            }
            else
            {
                ret = await _masterProductRepository.AddAsync(masterProduct, cancellationToken);
            }
            return ret;
        }

        /// <summary>
        /// Update master product
        /// </summary>
        /// <param name="id">The id of the master product</param>
        /// <param name="masterProduct">The updated master product</param>
        /// <param name="cancellationToken"></param>
        /// <returns>OneOf<Guid, List<ValidationFailure>, NotFound></returns>
        public async Task<OneOf<Guid, List<ValidationFailure>, NotFound>> UpdateBrandAsync(Guid id, MasterProduct masterProduct, CancellationToken cancellationToken)
        {
            OneOf<Guid, List<ValidationFailure>, NotFound> ret;

            //validation
            var validationResult = _masterProductValidator.Validate(masterProduct);
            if (!validationResult.IsValid)
            {
                ret = validationResult.Errors.ToList();
            }
            else
            {
                var supplierOption = await _masterProductRepository.UpdateAsync(id, masterProduct, cancellationToken: cancellationToken);
                ret = supplierOption.Match<OneOf<Guid, List<ValidationFailure>, NotFound>>(
                    f0: masterProductId => masterProductId,
                    f1: none => default(NotFound)
                    );

            }
            return ret;
        }

        public async Task<List<MasterProduct>> GetMasterProductsAsync(CancellationToken cancellationToken)
        {
            return await _masterProductRepository.GetManyAsync(cancellationToken: cancellationToken);

        }

        public async Task<OneOf<MasterProduct, NotFound>> GetMasterProductByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var option = await _masterProductRepository.GetByIdAsync(id, cancellationToken: cancellationToken);
            return option.Match<OneOf<MasterProduct, NotFound>>(
                f0: masterProduct => masterProduct,
                f1: none => default(NotFound)
                );

        }
    }
}
