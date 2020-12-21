using CA.ERP.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.MasterProductAgg
{
    public class MasterProduct: ModelBase
    {
        public string Model { get; set; }
        public string Description { get; set; }
        public Guid BrandId { get; set; }
        public Brand Brand { get; set; }
    }
}
