using CA.ERP.Domain.BrandAgg;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.SupplierAgg
{
    public class SupplierBrandValidator: AbstractValidator<SupplierBrand>
    {
        private readonly IBrandRepository _brandRepository;

        public SupplierBrandValidator(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
            RuleFor(t => t.BrandId).NotEmpty().CustomAsync(async (brandId, context, cancellationToken) => {
                var brandExist = await _brandRepository.ExistAsync(brandId);
                if (!brandExist)
                {
                    context.AddFailure("Brand should exist.");
                }
            });
        }

    }
}
