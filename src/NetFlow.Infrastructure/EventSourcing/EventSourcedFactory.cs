using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NetFlow.Infrastructure.Messaging;

namespace NetFlow.Infrastructure.EventSourcing
{
    public class EventSourcedFactory<TEventSourced> : IEventSourcedFactory<TEventSourced>
        where TEventSourced : IEventSourced
    {
        public TEventSourced Create(Guid id, IEnumerable<IEvent> sourcedEvents)
            =>
                (TEventSourced)
                    Activator.CreateInstance(typeof(TEventSourced), BindingFlags.Public, null,
                        new object[] {id, (sourcedEvents ?? Enumerable.Empty<IEvent>())});
    }
}