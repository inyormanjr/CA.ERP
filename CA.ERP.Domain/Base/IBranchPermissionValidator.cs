using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.Domain.Base
{
    public interface IBranchPermissionValidator
    {

    }
    public interface IBranchPermissionValidator<T> : IBranchPermissionValidator
    {
        Task<bool> HasPermissionAsync(T toValidate);
    }
}
