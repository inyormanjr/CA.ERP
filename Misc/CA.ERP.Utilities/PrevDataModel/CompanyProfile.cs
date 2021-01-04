using System;
using System.Collections.Generic;

#nullable disable

namespace CA.ERP.Utilities.PrevDataModel
{
    public partial class CompanyProfile
    {
        public int IndexNo { get; set; }
        public string CitiProfileNo { get; set; }
        public string CedulaName { get; set; }
        public string CedulaNo { get; set; }
        public DateTime? CedulaDateissued { get; set; }
        public string PlaceIssued { get; set; }
        public string BranchId { get; set; }
    }
}
