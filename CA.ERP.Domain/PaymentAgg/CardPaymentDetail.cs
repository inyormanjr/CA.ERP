using CA.ERP.Domain.BankAgg;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.PaymentAgg
{
    public class CardPaymentDetail
    {
        public Guid PaymentId { get; set; }
        public Guid BankId { get; set; }
        public string TransactionNumber { get; set; }
        public Payment Payment { get; set; }
        public Bank Bank { get; set; }
    }
}
