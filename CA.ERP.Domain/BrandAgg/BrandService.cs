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

namespace CA.ERP.Domain.BrandAgg
{
    public class BrandService : ServiceBase<Brand>
    {
        private readonly IBrandFactory _brandFactory;
        private readonly IValidator<Brand> _brandValidator;
        private readonly IBrandRepository _brandRepository;

        public BrandService(IUnitOfWork unitOfWork,IBrandFactory brandFactory, IValidator<Brand> brandValidator, IBrandRepository brandRepository, IUserHelper userHelper)
            : base(unitOfWork, brandRepository, brandValidator, userHelper)
        {
            _brandFactory = brandFactory;
            _brandValidator = brandValidator;
            _brandRepository = brandRepository;
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
            await _unitOfWork.CommitAsync();
            return ret;
        }
    }
}
