using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Dto
{
    /// <summary>
    /// Get multiple supplier response wrapper
    /// </summary>
    public class GetSuppliersResponse
    {
        /// <summary>
        /// List of suppliers. Can be empty but never null
        /// </summary>
        public ICollection<Supplier> Suppliers { get; set; }
    }
}
