using System;
using System.Collections.Generic;

#nullable disable

namespace CA.ERP.Utilities.PrevDataModel
{
    public partial class ChangesLog
    {
        public int Id { get; set; }
        public string ChangeType { get; set; }
        public string Details { get; set; }
        public DateTime? DateChange { get; set; }
    }
}
