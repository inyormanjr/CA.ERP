using System;

namespace CA.ERP.DataAccess.Entities
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