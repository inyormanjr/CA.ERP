using CA.ERP.Domain.Common;
using CA.ERP.Domain.Core;
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
            Status = Status.Active;
        }
        public Guid Id { get; set; }
        public Status Status { get; set; }
    }
}
