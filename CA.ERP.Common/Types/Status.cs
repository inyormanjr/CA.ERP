using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Common.Types
{
    [Flags]
    public enum Status : int
    {
        /// <summary>
        /// Consider the item deleted
        /// </summary>
        Inactive = 0,
        /// <summary>
        /// Should be the default status
        /// </summary>
        Active = 1,

        All = ~0,
    }
}
