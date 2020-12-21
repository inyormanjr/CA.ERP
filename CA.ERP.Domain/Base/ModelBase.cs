using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.Base
{
    public abstract class ModelBase
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
