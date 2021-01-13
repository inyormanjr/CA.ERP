using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace CA.ERP.Utilities.PrevDataModel
{
    public partial class PurchaseOrder
    {
        public int IndexNo { get; set; }
        public string PoId { get; set; }
        public DateTime? PoDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string SupIdno { get; set; }
        public string BranchNo { get; set; }
        public string ApprovedBy { get; set; }
        public string TotalAmount { get; set; }

        [NotMapped]
        public CA.ERP.DataAccess.Entities.PurchaseOrder NewPurchaseOrder { get; set; }
        public List<PoDetail> PoDetails { get; set; } = new List<PoDetail>();
    }
}
