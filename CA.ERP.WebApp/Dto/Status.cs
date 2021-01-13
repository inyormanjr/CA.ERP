using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Dto
{
    /// <summary>
    /// Enum status for dto
    /// </summary>
    [Flags]
    public enum Status:int
    {
        /// <summary>
        /// Consider the item deleted
        /// </summary>
        Inactive = 0,
        /// <summary>
        /// Should be the default status
        /// </summary>
        Active = 1
    }
}
