using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Common.Types
{
    /// <summary>
    /// Flags if product can be deleted
    /// </summary>
    public enum ProductStatus : int
    {
        /// <summary>
        /// Product has no other data that reference it and can be deleted.
        /// </summary>
        Provisional = 0,
        /// <summary>
        /// Product has no other data that reference it and should not be deleted
        /// </summary>
        Permanent = 1
    }
}
