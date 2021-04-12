using System;

namespace CA.ERP.DataAccess.Entities
{
    public class TransactionProduct : EntityBase
    {
        public Guid StockId { get; set; }
        public string DownPaymentOfficialReceiptNumber { get; set; }
        public decimal SalePrice { get; set; }
        public decimal Quantity { get; set; }
        public decimal DownPayment { get; set; }
        public string Remarks { get; set; }
    }
}