using CA.ERP.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.BrandAgg
{
    public interface IBrandFactory : IFactory<Brand>
    {
        Brand CreateBrand(string name, string description);
    }
}
