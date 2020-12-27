using CA.ERP.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.Base
{
    public abstract class ModelBase
    {
        public ModelBase()
        {

            CreatedBy = Guid.Empty;
            UpdatedBy = Guid.Empty;
            Status = Status.Active;
        }
        public Guid Id { get; set; }
        public Status Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid UpdatedBy { get; set; }
    }
}
