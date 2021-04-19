using CA.ERP.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.Shared.Dto
{
    /// <summary>
    /// Base class for dto poco classes
    /// </summary>
    public abstract class DtoViewBase
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public DtoViewBase()
        {
            Status = Status.Active;
        }
        /// <summary>
        /// The unique identifier for the data
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Active or Inactive consider the data deleted if inactive.
        /// </summary>
        public Status Status { get; set; }
    }
}
