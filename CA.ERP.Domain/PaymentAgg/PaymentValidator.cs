using CA.ERP.Domain.BranchAgg;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Domain.PaymentAgg
{
    public class PaymentValidator: AbstractValidator<Payment>
    {
        private readonly IBranchRepository _branchRepository;

        public PaymentValidator(IBranchRepository branchRepository, IValidator<CashPaymentDetail> cashPaymentDetailValidator, IValidator<CardPaymentDetail> cardPaymentDetailValidator, IValidator<ChequePaymentDetail> chequePaymentDetailValidator)
        {

            RuleFor(t => t.BranchId).MustAsync(BranchMustExist);
            RuleFor(t => t.TransactionId).NotEmpty();
            RuleFor(t => t.OfficialReceiptNumber).NotEmpty();
            RuleFor(t => t.PaymentDate).NotEmpty();
            RuleFor(t => t.GrossAmount).GreaterThanOrEqualTo(0);
            RuleFor(t => t.Rebate).GreaterThanOrEqualTo(0);
            RuleFor(t => t.Interest).GreaterThanOrEqualTo(0);
            RuleFor(t => t.Discount).GreaterThanOrEqualTo(0);
            RuleFor(t => t.NetAmount).GreaterThanOrEqualTo(0);

            RuleFor(t => t.CashPaymentDetail).SetValidator(cashPaymentDetailValidator);
            RuleFor(t => t.CardPaymentDetail).SetValidator(cardPaymentDetailValidator);
            RuleFor(t => t.ChequePaymentDetail).SetValidator(chequePaymentDetailValidator);


            _branchRepository = branchRepository;
        }

        private Task<bool> BranchMustExist(Guid branchId, CancellationToken cancellationToken)
        {
            return _branchRepository.ExistAsync(branchId, cancellationToken: cancellationToken);
        }
    }
}
