using System;
using System.Collections.Generic;
using NetFlow.Common.Messaging;

namespace NetFlow.Domain.Persistence
{
    public interface IEventStore
    {
        IEnumerable<IDomainEvent> GetEvents(Guid streamId);

        IEnumerable<Guid> Save(IEnumerable<IDomainEvent> events);
    }
}
