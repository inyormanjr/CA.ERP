using System;
using System.Collections.Generic;

#nullable disable

namespace CA.ERP.Utilities.PrevDataModel
{
    public partial class CustomerTable
    {
        public string IdNumber { get; set; }
        public string LastName { get; set; }
        public string MidName { get; set; }
        public string FName { get; set; }
        public string Employer { get; set; }
        public string Contact { get; set; }
        public string CoMaker { get; set; }
        public string Address { get; set; }
        public string CoMakerAddress { get; set; }
        public string CivilStatus { get; set; }
        public string EmployerAddress { get; set; }
        public string FullName { get; set; }
        public DateTime? DateRegistered { get; set; }
        public string RegisteredBy { get; set; }
        public int? Id { get; set; }
    }
}
