using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.MasterProductAgg
{
    public class MasterProductValidator : AbstractValidator<MasterProduct>
    {
        public MasterProductValidator()
        {
            RuleFor(m => m.Model).NotEmpty();
            RuleFor(m => m.BrandId).NotEqual(Guid.Empty).WithMessage("Brand Id should not be empty");
        }
    }
}
