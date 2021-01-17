using System;

namespace CA.ERP.DataAccess.Entities
{
    public class CashPaymentDetail
    {
        public Guid PaymentId { get; set; }
        public decimal TenderAmount { get; set; }
        public decimal Change { get; set; }
        public Payment Payment { get; set; }
    }
}