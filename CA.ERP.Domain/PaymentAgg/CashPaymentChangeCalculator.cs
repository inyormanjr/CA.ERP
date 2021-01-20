using CA.ERP.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.PaymentAgg
{
    public class CashPaymentChangeCalculator: IHelper
    {
        public void Calculate(Payment payment)
        {
            if (payment.CashPaymentDetail != null)
            {
                payment.CashPaymentDetail.Change = payment.CashPaymentDetail.TenderAmount - payment.NetAmount;
            }
            
        }
    }
}
