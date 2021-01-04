using System;
using System.Collections.Generic;

#nullable disable

namespace CA.ERP.Utilities.PrevDataModel
{
    public partial class DeliveryReceipt
    {
        public int Id { get; set; }
        public string AccountNo { get; set; }
        public string AccountType { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string SiDrNo { get; set; }
        public string Salesman { get; set; }
        public string CiBy { get; set; }
        public string IdNumber { get; set; }
        public string CollectionId { get; set; }
        public string Status { get; set; }
        public string BranchNo { get; set; }
        public string DrNo { get; set; }
        public string Drtype { get; set; }
    }
}
