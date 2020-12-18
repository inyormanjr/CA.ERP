using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.DTO
{
    public class RegisterRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int BranchId { get; set; }

    }
}
