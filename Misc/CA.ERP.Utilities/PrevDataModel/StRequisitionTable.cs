using System;
using System.Collections.Generic;

#nullable disable

namespace CA.ERP.Utilities.PrevDataModel
{
    public partial class StRequisitionTable
    {
        public string StId { get; set; }
        public DateTime? DateTransaction { get; set; }
        public string TransferLocation { get; set; }
        public string GatePassNo { get; set; }
        public string ReceivedBy { get; set; }
        public string ReleasedBy { get; set; }
        public string FromLocation { get; set; }
    }
}
