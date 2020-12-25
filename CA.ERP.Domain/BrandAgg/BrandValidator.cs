using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.BrandAgg
{
    public class BrandValidator : AbstractValidator<Brand>
    {
        public BrandValidator()
        {
            RuleFor(b => b.Name).NotEmpty();
        }
    }
}
