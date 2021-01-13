using CA.ERP.Domain.BrandAgg;
using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Domain.MasterProductAgg
{
    public class MasterProductValidator : AbstractValidator<MasterProduct>
    {
        private readonly IBrandRepository _brandRepository;

        public MasterProductValidator(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;

            RuleFor(m => m.Model).NotEmpty();
            RuleFor(m => m.BrandId).CustomAsync(BrandExist);
        }


        private async Task BrandExist(Guid id, CustomContext context, CancellationToken cancellationToken)
        {
            var exist = await _brandRepository.ExistAsync(id);
            if (!exist)
            {
                context.AddFailure("Brand should exist.");
            }
        }
    }
}
