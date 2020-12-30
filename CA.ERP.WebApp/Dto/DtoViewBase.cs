using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CA.ERP.WebApp.Dto
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
            CreatedBy = Guid.Empty;
            UpdatedBy = Guid.Empty;
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
        /// <summary>
        /// Date the data was created
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// The unique identifier for the user who created the data.
        /// </summary>
        public Guid CreatedBy { get; set; }
        /// <summary>
        /// The date the data was last updated.
        /// </summary>
        public DateTime UpdatedAt { get; set; }
        /// <summary>
        /// The unique identifier for the user who last updated the data.
        /// </summary>
        public Guid UpdatedBy { get; set; }
    }
}
