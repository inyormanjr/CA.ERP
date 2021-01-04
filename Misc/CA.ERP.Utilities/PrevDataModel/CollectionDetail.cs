using System;
using System.Collections.Generic;

#nullable disable

namespace CA.ERP.Utilities.PrevDataModel
{
    public partial class CollectionDetail
    {
        public int Id { get; set; }
        public string CollectionDetailsId { get; set; }
        public DateTime? Date { get; set; }
        public string No { get; set; }
        public string OrNumber { get; set; }
        public string PrinAmount { get; set; }
        public string TotalAmount { get; set; }
        public string Penalty { get; set; }
        public string Rebate { get; set; }
        public string Discount { get; set; }
        public string AcctBalance { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string CollectionId { get; set; }
        public DateTime? ExpectedDate { get; set; }
        public string ExpectedBalance { get; set; }
        public string PaymentType { get; set; }
        public string Remarks { get; set; }
        public string StockNo { get; set; }
        public string CollCashAmt { get; set; }
        public string ColChequeAmt { get; set; }
    }
}
