using CA.ERP.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.PaymentAgg
{
    public class Payment : ModelBase
    {
        public Guid TransactionId { get; set; }
        public Guid BranchId { get; set; }
        public string OfficialReceiptNumber { get; set; }
        public DateTime PaymentDate { get; set; }
        public TransactionType TransactionType { get; set; }
        public PaymentType PaymentType { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal Rebate { get; set; }
        public decimal Interest { get; set; }
        public decimal Discount { get; set; }
        public decimal NetAmount { get; set; }
        public string Remarks { get; set; }

        public CashPaymentDetail CashPaymentDetail { get; set; }
        public CardPaymentDetail CardPaymentDetail { get; set; }
        public ChequePaymentDetail ChequePaymentDetail { get; set; }
    }
}
