using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Dto.Supplier
{
    public class SupplierCreate
    {
        /// <summary>
        /// Name of the supplier.
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Adress of the supplier
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// The name of the contact person for the supplier
        /// </summary>
        public string ContactPerson { get; set; }

        public List<SupplierBrandCreate> SupllierBrands { get; set; } = new List<SupplierBrandCreate>();
    }
}
