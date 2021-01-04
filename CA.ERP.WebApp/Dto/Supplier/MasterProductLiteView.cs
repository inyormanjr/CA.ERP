using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Dto.Supplier
{
    public class MasterProductLiteView
    {
        /// <summary>
        /// Master Product Id
        /// </summary>
        public Guid Id { get; set; }
        public string Model { get; set; }
        public decimal CostPrice { get; set; }
    }
}
