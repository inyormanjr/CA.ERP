using System;
using System.Collections.Generic;
using System.Text;

namespace CA.ERP.Domain.Core.Entity
{
    public interface IEntity
    {
        public Guid Id { get;  }
        public Status Status { get;  }

    }
}
