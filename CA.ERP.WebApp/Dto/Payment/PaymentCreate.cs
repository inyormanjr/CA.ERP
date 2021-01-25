using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Dto.Payment
{
    public class PaymentCreate
    {
        public Guid BranchId { get; set; }
        public string OfficialReceiptNumber { get; set; }
        public Domain.PaymentAgg.PaymentType PaymentType { get; set; }
        public Domain.PaymentAgg.PaymentMethod PaymentMethod { get; set; }
        public DateTime PaymentDate { get; set; }

        public decimal GrossAmount { get; set; }
        public decimal Rebate { get; set; }
        public decimal Interest { get; set; }
        public decimal Discount { get; set; }
        public string Remarks { get; set; }
        public decimal TenderAmount { get; set; }
        public Guid BankId { get; set; }
        public string TransactionNumber { get; set; }

    }
}
