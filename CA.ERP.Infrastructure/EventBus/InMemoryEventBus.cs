using CA.ERP.Domain.Core.EventBus;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.ERP.Infrastructure.EventBus
{
    public class InMemoryEventBus : IEventBus
    {
        private readonly IMediator _mediator;

        public InMemoryEventBus(IMediator mediator)
        {
            _mediator = mediator;
        }
        public Task Raised(IDomainEvent domainEvent, CancellationToken cancellationToken = default)
        {
            return _mediator.Publish(domainEvent, cancellationToken);
        }
    }
}
