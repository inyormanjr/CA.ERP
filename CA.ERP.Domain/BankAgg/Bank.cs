using CA.ERP.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.BankAgg
{
    public class Bank: ModelBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
