using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.DataAccess.Entities
{
    public class SupplierBrand : EntityBase
    {
        public Guid SupplierId { get; set; }
        public Guid BrandId { get; set; }
        public Supplier Supplier { get; set; }
        public Brand Brand { get; set; }
    }
}
