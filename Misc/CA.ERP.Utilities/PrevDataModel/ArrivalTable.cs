using System;
using System.Collections.Generic;

#nullable disable

namespace CA.ERP.Utilities.PrevDataModel
{
    public partial class ArrivalTable
    {
        public string DeliveryNo { get; set; }
        public string Via { get; set; }
        public string Waybill { get; set; }
        public DateTime? DateReceived { get; set; }
        public DateTime? DatePrinted { get; set; }
        public string BranchNo { get; set; }
        public string SName { get; set; }
    }
}
