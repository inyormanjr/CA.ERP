using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.Domain.IdentityAgg
{
    public interface IIdentityProvider
    {
        Task<Identity> GetCurrentIdentity();
    }
}
