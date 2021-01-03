using System;
using System.Collections.Generic;

#nullable disable

namespace CA.ERP.Utilities.PrevDataModel
{
    public partial class TransferTable
    {
        public string TransferId { get; set; }
        public string StockNo { get; set; }
        public DateTime? TransDate { get; set; }
        public string TransferLocation { get; set; }
        public string Qnty { get; set; }
        public string StId { get; set; }
    }
}
