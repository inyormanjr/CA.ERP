using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.Core
{
    public enum Status : int
    {
        Inactive = 0,
        Active = 1,
        All = ~0
    }
}
