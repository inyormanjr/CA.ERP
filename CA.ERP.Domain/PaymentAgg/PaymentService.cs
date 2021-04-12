using CA.ERP.Domain.Base;
using CA.ERP.Domain.Common.ResultTypes;
using CA.ERP.Domain.UnitOfWorkAgg;
using CA.ERP.Domain.UserAgg;
using FluentValidation;
using FluentValidation.Results;
using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Domain.PaymentAgg
{
    public class PaymentService : ServiceBase<Payment>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly PaymentNetTotalCalculator _paymentNetTotalCalculator;
        private readonly CashPaymentChangeCalculator _cashPaymentChangeCalculator;
        private readonly IBranchPermissionValidator<Payment> _paymentBranchPermissionValidator;

        public PaymentService(
            IUnitOfWork unitOfWork,
            IPaymentRepository paymentRepository,
            IValidator<Payment> validator,
            IUserHelper userHelper,
            PaymentNetTotalCalculator paymentNetTotalCalculator,
            CashPaymentChangeCalculator cashPaymentChangeCalculator,
            IBranchPermissionValidator<Payment> paymentBranchPermissionValidator)
            : base(unitOfWork, paymentRepository, validator, userHelper)
        {
            _paymentRepository = paymentRepository;
            _paymentNetTotalCalculator = paymentNetTotalCalculator;
            _cashPaymentChangeCalculator = cashPaymentChangeCalculator;
            _paymentBranchPermissionValidator = paymentBranchPermissionValidator;
        }

        public async Task<OneOf<Guid, List<ValidationFailure>, Forbidden>> Create(Guid branchId, string officialReceiptNumber, PaymentType paymentType, PaymentMethod paymentMethod, DateTime paymentDate, decimal grossAmount, decimal rebate, decimal interest, decimal discount, string remarks, decimal tenderAmount = 0, Guid bankId = default(Guid), string transactionNumber = null, CancellationToken cancellationToken = default)
        {
            OneOf<Guid, List<ValidationFailure>, Forbidden> ret;

            var payment = new Payment()
            {
                BranchId = branchId,
                OfficialReceiptNumber = officialReceiptNumber,
                PaymentType = paymentType,
                PaymentMethod = paymentMethod,
                PaymentDate = paymentDate,
                GrossAmount = grossAmount,
                Rebate = rebate,
                Interest = interest,
                Discount = discount,
                Remarks = remarks,
            };

            //comoute net amount
            _paymentNetTotalCalculator.Calculate(payment);

            if (paymentMethod == PaymentMethod.Cash)
            {
                payment.CashPaymentDetail = new CashPaymentDetail()
                {
                    TenderAmount = tenderAmount,
                };
                _cashPaymentChangeCalculator.Calculate(payment);
            }
            //validate branch
            bool hasBranchPermission = await _paymentBranchPermissionValidator.HasPermissionAsync(payment, cancellationToken);
            if (!hasBranchPermission)
            {
                ret = default(Forbidden);
            }
            else
            {
                var validationResult = await _validator.ValidateAsync(payment, cancellationToken);
                if (!validationResult.IsValid)
                {
                    ret = validationResult.Errors.ToList();
                }
                else
                {
                    ret = await _paymentRepository.AddAsync(payment, cancellationToken);
                    await _unitOfWork.CommitAsync(cancellationToken);
                }
            }
            return ret;
        }
    }
}
