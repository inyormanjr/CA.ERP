using CA.ERP.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.MasterProductAgg
{
    public class Brand: ModelBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<MasterProduct> MasterProducts { get; set; }

    }
}
