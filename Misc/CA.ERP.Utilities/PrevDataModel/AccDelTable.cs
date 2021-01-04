using System;
using System.Collections.Generic;

#nullable disable

namespace CA.ERP.Utilities.PrevDataModel
{
    public partial class AccDelTable
    {
        public int Id { get; set; }
        public DateTime? DelDateTime { get; set; }
        public string DelBy { get; set; }
        public string UserId { get; set; }
        public string AccountNo { get; set; }
    }
}
