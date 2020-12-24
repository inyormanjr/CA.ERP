using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CA.ERP.WebApp.Dto
{
    /// <summary>
    /// Supplier Dto
    /// </summary>
    public class Supplier
    {
        /// <summary>
        /// The name of the supplier.
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

        /// <summary>
        /// Brands available to the supplier
        /// </summary>
        public List<SupplierBrand> SupllierBrands { get; set; } = new List<SupplierBrand>();
    }
}