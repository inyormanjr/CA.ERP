using CA.ERP.Domain.Core.EventBus;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Infrastructure.EventBus
{
    public class MassTransitEventBus : IEventBus
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public MassTransitEventBus(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }
        public Task Publish(IDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            return _publishEndpoint.Publish((dynamic)domainEvent, cancellationToken);
        }
    }
}
