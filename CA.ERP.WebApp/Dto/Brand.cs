using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Dto
{
    public class Brand : DtoViewBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<MasterProduct> MasterProducts { get; set; }
    }
}
