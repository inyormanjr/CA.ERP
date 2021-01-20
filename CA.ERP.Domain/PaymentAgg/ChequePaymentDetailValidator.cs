using CA.ERP.Domain.BankAgg;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Domain.PaymentAgg
{
    public class ChequePaymentDetailValidator : AbstractValidator<ChequePaymentDetail>
    {
        private readonly IBankRepository _bankRepository;
        private readonly IPaymentRepository _paymentRepository;

        public ChequePaymentDetailValidator(IBankRepository bankRepository, IPaymentRepository paymentRepository)
        {
            RuleFor(t => t.BankId).MustAsync(BankMustExist);
            RuleFor(t => t.ChequeNumber).NotEmpty().MustAsync(ChequeNumberMustNotExist); ;


            _bankRepository = bankRepository;
            _paymentRepository = paymentRepository;
        }

        private async Task<bool> ChequeNumberMustNotExist(string chequeNumber, CancellationToken cancellationToken)
        {
            return !await _paymentRepository.ChequeNumberExistAsync(chequeNumber, cancellationToken);
        }

        private Task<bool> BankMustExist(Guid bankId, CancellationToken cancellationToken)
        {
            return _bankRepository.ExistAsync(bankId);
        }
    }
}
