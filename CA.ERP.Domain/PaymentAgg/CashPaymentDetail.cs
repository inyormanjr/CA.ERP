using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.PaymentAgg
{
    public class CashPaymentDetail
    {
        public Guid PaymentId { get; set; }
        public decimal TenderAmount { get; set; }
        public decimal Change { get; set; }
        public Payment Payment { get; set; }
    }
}
