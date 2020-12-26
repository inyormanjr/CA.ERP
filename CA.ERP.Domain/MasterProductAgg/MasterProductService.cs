using CA.ERP.Domain.Base;
using FluentValidation;
using FluentValidation.Results;
using OneOf;
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

    }
}
