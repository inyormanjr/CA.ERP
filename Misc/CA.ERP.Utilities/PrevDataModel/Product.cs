using System;
using System.Collections.Generic;

#nullable disable

namespace CA.ERP.Utilities.PrevDataModel
{
    public partial class Product
    {
        public string StockNo { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string SerialNo { get; set; }
        public string Status { get; set; }
        public string PoDetailsId { get; set; }
        public string ProdStatus { get; set; }
        public string DeliveryNo { get; set; }
        public string LocFrom { get; set; }
        public string LocTo { get; set; }
        public DateTime? DateReceived { get; set; }
        public string StId { get; set; }
        public string Price { get; set; }
        public string BranchNo { get; set; }
    }
}
