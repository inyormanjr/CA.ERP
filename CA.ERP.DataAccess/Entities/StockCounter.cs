using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.DataAccess.Entities
{
    public class StockCounter
    {
        public string Code { get; set; }
        public int Counter { get; set; }
        public int Year { get; set; }
    }
}
