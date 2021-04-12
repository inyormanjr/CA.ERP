using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Domain.Core.EventBus
{
    public interface IEventBus
    {
        Task Publish(IDomainEvent domainEvent, CancellationToken cancellationToken);
    }
}
