using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.DataAccess.Entities
{
    public class Payment : EntityBase
    {
        public Guid TransactionId { get; set; }
        public Guid BranchId { get; set; }
        public string OfficialReceiptNumber { get; set; }
        public DateTime PaymentDate { get; set; }
        public Domain.PaymentAgg.PaymentType PaymentType { get; set; }
        public Domain.PaymentAgg.PaymentMethod PaymentMethod { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal Rebate { get; set; }
        public decimal Interest { get; set; }
        public decimal Discount { get; set; }
        public decimal NetAmount { get; set; }
        public string Remarks { get; set; }

        public CashPaymentDetail CashPaymentDetail { get; set; }
        public CardPaymentDetail CardPaymentDetail { get; set; }
        public ChequePaymentDetail ChequePaymentDetail { get; set; }
        public Transaction Transaction { get;  set; }
    }
}
