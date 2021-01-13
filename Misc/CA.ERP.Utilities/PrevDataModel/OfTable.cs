using System;
using System.Collections.Generic;

#nullable disable

namespace CA.ERP.Utilities.PrevDataModel
{
    public partial class OfTable
    {
        public string Ofid { get; set; }
        public string FeeType { get; set; }
        public string Ornumber { get; set; }
        public string Remarks { get; set; }
        public int? TransId { get; set; }
        public string Payments { get; set; }
        public string PaymentType { get; set; }
        public string CashAmt { get; set; }
        public string ChequeAmt { get; set; }
    }
}
