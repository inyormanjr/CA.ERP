using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Dto.Payment
{
    public class PaymentCreate
    {
        public Guid BrandId { get; set; }
        public string OfficialReceiptNumber { get; set; }
        public Domain.PaymentAgg.PaymentType PaymentType { get; set; }
        public Domain.PaymentAgg.PaymentMethod PaymentMethod { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal Rebate { get; set; }
        public decimal Interest { get; set; }
        public decimal Remarks { get; set; }
        public decimal TenderAmount { get; set; }
        public Guid BankId { get; set; }
        public string TransactionNumber { get; set; }
    }
}
