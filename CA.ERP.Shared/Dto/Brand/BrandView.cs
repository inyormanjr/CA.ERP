using CA.ERP.WebApp.Dto.MasterProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Dto.Brand
{
    public class BrandView : DtoViewBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<MasterProductView> MasterProducts { get; set; }
    }
}
