using System.Collections.Generic;

namespace CA.ERP.DataAccess.Entities
{
    public class Bank : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<CardPaymentDetail> CardPaymentDetails { get; set; }
        public List<ChequePaymentDetail> ChequePaymentDetails { get; set; }
    }
}