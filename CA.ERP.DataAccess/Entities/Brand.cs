using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.DataAccess.Entities
{
    public class Brand : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<MasterProduct> MasterProducts { get; set; }
        public List<SupplierBrand> SupplierBrands { get; set; }

    }
}
