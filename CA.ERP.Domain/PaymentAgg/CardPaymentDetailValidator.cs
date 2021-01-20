using CA.ERP.Domain.BankAgg;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Domain.PaymentAgg
{
    public class CardPaymentDetailValidator : AbstractValidator<CardPaymentDetail>
    {
        private readonly IBankRepository _bankRepository;
        private readonly IPaymentRepository _paymentRepository;

        public CardPaymentDetailValidator(IBankRepository bankRepository, IPaymentRepository paymentRepository)
        {
            RuleFor(t => t.BankId).MustAsync(BankMustExist);
            RuleFor(t => t.TransactionNumber).NotEmpty().MustAsync(TransactionNumberMustNotExist); ;


            _bankRepository = bankRepository;
            _paymentRepository = paymentRepository;
        }

        private async Task<bool> TransactionNumberMustNotExist(string transactionNumber, CancellationToken cancellationToken)
        {
            return !await _paymentRepository.TransactionNumberExistAsync(transactionNumber, cancellationToken);
        }

        private Task<bool> BankMustExist(Guid bankId, CancellationToken cancellationToken)
        {
            return _bankRepository.ExistAsync(bankId);
        }
    }
}
