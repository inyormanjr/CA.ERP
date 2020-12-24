using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.SupplierAgg
{
    public class SupplierValidator : AbstractValidator<Supplier>
    {
        public SupplierValidator()
        {
            RuleFor(s => s.Name).NotEmpty().WithMessage("Name is required.");
        }
    }
}
