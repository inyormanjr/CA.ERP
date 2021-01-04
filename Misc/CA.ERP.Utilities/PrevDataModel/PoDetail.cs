using System;
using System.Collections.Generic;

#nullable disable

namespace CA.ERP.Utilities.PrevDataModel
{
    public partial class PoDetail
    {
        public int IndexNo { get; set; }
        public string PoDetailsId { get; set; }
        public string Model { get; set; }
        public string OrderedQty { get; set; }
        public string FreeQty { get; set; }
        public string Cost { get; set; }
        public string Discount { get; set; }
        public string TotalCost { get; set; }
        public string PoId { get; set; }
        public string Brand { get; set; }
        public string TotalQty { get; set; }
        public string Status { get; set; }
        public string RemainingQty { get; set; }
        public string HolderQty { get; set; }
    }
}
