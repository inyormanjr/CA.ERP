using CA.ERP.Domain.Base;
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

namespace CA.ERP.Domain.BrandAgg
{
    public class BrandService : ServiceBase
    {
        private readonly IBrandFactory _brandFactory;
        private readonly IValidator<Brand> _brandValidator;
        private readonly IBrandRepository _brandRepository;
        private readonly IUserHelper _userHelper;

        public BrandService(IBrandFactory brandFactory, IValidator<Brand> brandValidator, IBrandRepository brandRepository, IUserHelper userHelper)
        {
            _brandFactory = brandFactory;
            _brandValidator = brandValidator;
            _brandRepository = brandRepository;
            _userHelper = userHelper;
        }

        public async Task<OneOf<Guid, List<ValidationFailure>>> CreateBrandAsync(string name, string description, CancellationToken cancellationToken)
        {
            OneOf<Guid, List<ValidationFailure>> ret;
            var brand = _brandFactory.CreateBrand(name, description);
            var validationResult = _brandValidator.Validate(brand);
            if (!validationResult.IsValid)
            {
                ret = validationResult.Errors.ToList();
            }
            else
            {
                brand.CreatedBy = _userHelper.GetCurrentUserId();
                brand.UpdatedBy = _userHelper.GetCurrentUserId();
                ret = await _brandRepository.AddAsync(brand, cancellationToken: cancellationToken);
            }
            return ret;
        }

        public async Task<OneOf<Guid, List<ValidationFailure>, NotFound>> UpdateBrandAsync(Guid id, Brand brand, CancellationToken cancellationToken)
        {
            OneOf<Guid, List<ValidationFailure>, NotFound> ret;

            //validation
            var validationResult = _brandValidator.Validate(brand);
            if (!validationResult.IsValid)
            {
                ret = validationResult.Errors.ToList();
            }
            else
            {
                var supplierOption = await _brandRepository.UpdateAsync(id, brand, cancellationToken: cancellationToken);
                ret = supplierOption.Match<OneOf<Guid, List<ValidationFailure>, NotFound>>(
                    f0: supplierId => supplierId,
                    f1: none => default(NotFound)
                    );

            }
            return ret;
        }

        public async Task<List<Brand>> GetBrandsAsync(CancellationToken cancellationToken)
        {
            return await _brandRepository.GetManyAsync(cancellationToken: cancellationToken);
        }

        public async Task<OneOf<Brand, NotFound>> GetBrandByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var brandOption = await _brandRepository.GetByIdAsync(id, cancellationToken: cancellationToken);
            return brandOption.Match<OneOf<Brand, NotFound>>(
                f0: brand => brand,
                f1: none => default(NotFound)
                );
        }
    }
}
