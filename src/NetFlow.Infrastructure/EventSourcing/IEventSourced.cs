using System;
using System.Collections.Generic;
using NetFlow.Infrastructure.Messaging;

namespace NetFlow.Infrastructure.EventSourcing
{
    public interface IEventSourced
    {
        Guid Id { get; }

        int Version { get; }

        IEnumerable<IEvent> Events { get; }
    }
}
