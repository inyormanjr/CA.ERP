using CA.ERP.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.PaymentAgg
{
    public class PaymentNetTotalCalculator : IHelper
    {
        public void Calculate(Payment payment)
        {
            payment.NetAmount = payment.GrossAmount - payment.Rebate + payment.Interest - payment.Discount;
        }
    }
}
