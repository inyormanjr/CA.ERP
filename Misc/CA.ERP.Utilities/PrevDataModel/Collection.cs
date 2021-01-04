using System;
using System.Collections.Generic;

#nullable disable

namespace CA.ERP.Utilities.PrevDataModel
{
    public partial class Collection
    {
        public int Id { get; set; }
        public string CollectionId { get; set; }
        public string TotalLcp { get; set; }
        public string Payment { get; set; }
        public string Balance { get; set; }
        public string Udi { get; set; }
        public string TotalRebate { get; set; }
        public string Pn { get; set; }
        public string GrsMonthly { get; set; }
        public string NetMonthly { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string NotarialFee { get; set; }
        public string DeliveryFee { get; set; }
        public string OtherPay { get; set; }
        public string Status { get; set; }
        public string Terms { get; set; }
        public string PaymentType { get; set; }
        public string Remarks { get; set; }
        public string MonthlyRebate { get; set; }
    }
}
