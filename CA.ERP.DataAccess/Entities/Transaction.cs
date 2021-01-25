using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.DataAccess.Entities
{
    public class Transaction: EntityBase
    {
        public string AccountNumber { get; set; }
        public List<Payment> Payments { get;  set; }
    }
}
