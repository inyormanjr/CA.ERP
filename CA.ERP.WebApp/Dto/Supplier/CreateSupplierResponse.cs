using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Dto.Supplier
{
    public class CreateSupplierResponse : CreateResponse
    {
        /// <summary>
        /// The suppliers name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Adress of the supplier
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// The name of the contact person for the supplier
        /// </summary>
        public string ContactPerson { get; set; }
    }
}
