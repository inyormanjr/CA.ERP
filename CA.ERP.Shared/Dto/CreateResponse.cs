using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.Shared.Dto
{
    /// <summary>
    /// The default response for creating data.
    /// </summary>
    public class CreateResponse
    {
        /// <summary>
        /// Id of the created data.
        /// </summary>
        public Guid Id { get; set; }
    }
}
