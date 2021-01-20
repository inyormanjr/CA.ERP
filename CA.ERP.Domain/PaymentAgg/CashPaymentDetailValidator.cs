using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.PaymentAgg
{
    public class CashPaymentDetailValidator : AbstractValidator<CashPaymentDetail>
    {
        public CashPaymentDetailValidator()
        {
            RuleFor(t => t.TenderAmount).GreaterThanOrEqualTo(0);
            RuleFor(t => t.Change).GreaterThanOrEqualTo(0);
        }
    }
}
