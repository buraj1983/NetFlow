using System;
using System.Collections.Generic;
using NetFlow.Infrastructure.Messaging;

namespace NetFlow.Infrastructure.EventSourcing
{
    public interface IEventStore : IDisposable
    {
        void Save(IEnumerable<IEvent> events);

        IEnumerable<IEvent> GetById(Guid sourceId);
    }
}
