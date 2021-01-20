using CA.ERP.Domain.Base;
using CA.ERP.Domain.UnitOfWorkAgg;
using CA.ERP.Domain.UserAgg;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.Domain.PaymentAgg
{
    //public class PaymentService : ServiceBase<Payment>
    //{
    //    public PaymentService(IUnitOfWork unitOfWork, IRepository<Payment> repository, IValidator<Payment> validator, IUserHelper userHelper) : base(unitOfWork, repository, validator, userHelper)
    //    {
    //    }

    //    public async Task<Guid> Create(Guid branchId, string officialReceiptNumber, PaymentType paymentType, PaymentMethod paymentMethod, decimal grossAmount, decimal rebate, decimal interest, string remarks, decimal tenderAmount = 0, Guid bankId = default(Guid), string transactionNumber =  null)
    //    {
    //        var payment = new Payment() {
    //            BranchId = branchId,
    //            OfficialReceiptNumber = officialReceiptNumber,
    //            PaymentType = paymentType,
    //            PaymentMethod = paymentMethod,
    //            GrossAmount = grossAmount,
    //            Rebate = rebate,
    //            Interest = interest,
    //            Remarks = remarks,
    //        };
    //        if (paymentMethod == PaymentMethod.Cash)
    //        {
    //            payment.CashPaymentDetail = new CashPaymentDetail() { 
    //                TenderAmount = tenderAmount,
    //            };

    //        }
    //        return Guid.Empty;
    //    }
    //}
}
