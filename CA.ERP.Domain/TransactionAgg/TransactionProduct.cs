using System;
using CA.ERP.Domain.Base;

namespace CA.ERP.Domain.TransactionAgg
{
    public class TransactionProduct : ModelBase
    {
        public Guid StockId { get; set; }
        public string DownPaymentOfficialReceiptNumber { get; set; }
        public decimal SalePrice { get; set; }
        public decimal Quantity { get; set; }
        public decimal DownPayment { get; set; }
        public string Remarks { get; set; }
    }
}