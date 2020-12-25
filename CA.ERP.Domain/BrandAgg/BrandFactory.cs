using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.BrandAgg
{
    public class BrandFactory : IBrandFactory
    {
        public Brand CreateBrand(string name, string description)
        {
            return new Brand() { Name = name, Description = description };
        }
    }
}
