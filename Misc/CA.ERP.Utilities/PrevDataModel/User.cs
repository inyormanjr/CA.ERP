using System;
using System.Collections.Generic;

#nullable disable

namespace CA.ERP.Utilities.PrevDataModel
{
    public partial class User
    {
        public int IndexNo { get; set; }
        public string UserId { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Role { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string BranchNo { get; set; }
    }
}
