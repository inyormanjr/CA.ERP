using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.UserAgg
{
    public interface IUserHelper
    {
        Guid GetCurrentUserId();
    }
}
