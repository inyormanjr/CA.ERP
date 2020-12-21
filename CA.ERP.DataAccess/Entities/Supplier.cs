using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.DataAccess.Entities
{
    public class Supplier : EntityBase
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }

        public List<SupplierBrand> SupllierBrands { get; set; } = new List<SupplierBrand>();
    }
}
