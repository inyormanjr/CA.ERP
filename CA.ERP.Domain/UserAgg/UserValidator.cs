using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.UserAgg
{
    public class UserValidator: AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u => u.Username).NotEmpty().WithMessage("Username is required.");
            RuleFor(u => u.FirstName).NotEmpty().WithMessage("FirstName is required.");
            RuleFor(u => u.LastName).NotEmpty().WithMessage("LastName is required.");
            RuleFor(u => u.Role).NotEqual(UserRole.None).WithMessage("Role is required.");
        }
    }
}
