using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.BranchAgg
{
    public class BranchValidator: AbstractValidator<Branch>
    {
        public BranchValidator()
        {
            RuleFor(b => b.Name).NotEmpty();
            RuleFor(b => b.BranchNo).NotEmpty();
            RuleFor(b => b.Code).NotEmpty();
        }
    }
}
