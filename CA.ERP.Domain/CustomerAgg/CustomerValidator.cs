using FluentValidation;

namespace CA.ERP.Domain.CustomerAgg
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(t=>t.FirstName).NotEmpty();
            RuleFor(t=>t.LastName).NotEmpty();
        }
    }
}