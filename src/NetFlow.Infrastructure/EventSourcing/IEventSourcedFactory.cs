using System;
using System.Collections.Generic;
using NetFlow.Infrastructure.Messaging;

namespace NetFlow.Infrastructure.EventSourcing
{
    public interface IEventSourcedFactory<TEventSourced> where TEventSourced : IEventSourced
    {
        TEventSourced Create(Guid id, IEnumerable<IEvent> sourcedEvents);

    }
}
