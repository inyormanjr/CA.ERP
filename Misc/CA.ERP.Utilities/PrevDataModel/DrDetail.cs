using System;
using System.Collections.Generic;

#nullable disable

namespace CA.ERP.Utilities.PrevDataModel
{
    public partial class DrDetail
    {
        public int Id { get; set; }
        public string DrDetailsId { get; set; }
        public string Lcp { get; set; }
        public string StockNo { get; set; }
        public string AccountNo { get; set; }
        public string Qnty { get; set; }
        public string Remarks { get; set; }
        public string DownPayment { get; set; }
        public string OrNumber { get; set; }
        public string PaymentType { get; set; }
        public string Description { get; set; }
        public string PStatus { get; set; }
        public string Cash { get; set; }
        public string OrAmt { get; set; }
        public string Model { get; set; }
        public string SerialNo { get; set; }
        public string Brand { get; set; }
        public string Pn { get; set; }
        public string BalanceAf { get; set; }
        public string TermsDr { get; set; }
    }
}
