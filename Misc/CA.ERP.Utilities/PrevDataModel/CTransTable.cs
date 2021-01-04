using System;
using System.Collections.Generic;

#nullable disable

namespace CA.ERP.Utilities.PrevDataModel
{
    public partial class CTransTable
    {
        public int TransId { get; set; }
        public string TransType { get; set; }
        public string OrNum { get; set; }
        public DateTime? TransDate { get; set; }
        public string AccountNo { get; set; }
        public string GrsAmt { get; set; }
        public string Rebate { get; set; }
        public string Int { get; set; }
        public string Disc { get; set; }
        public string NetAmt { get; set; }
        public string PayType { get; set; }
        public string Payment { get; set; }
        public string Bank { get; set; }
        public string ChequeNo { get; set; }
        public string CTransStatus { get; set; }
        public string Change { get; set; }
        public string IdNumber { get; set; }
        public string BranchNo { get; set; }
        public string CashAmt { get; set; }
        public string ChequeAmt { get; set; }
        public string Remarks { get; set; }
        public DateTime? SystemDate { get; set; }
    }
}
