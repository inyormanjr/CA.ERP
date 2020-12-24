using CA.ERP.DataAccess.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA.ERP.DataAccess.Entities
{
    public abstract class EntityBase
    {
        public EntityBase()
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
