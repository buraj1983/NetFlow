using System;
using System.Collections.Generic;
using System.Linq;
using NetFlow.Infrastructure.Messaging;

namespace NetFlow.Infrastructure.EventSourcing
{
    public class EventSourcedRepository<TEventSourced> : IEventSourcedRepository<TEventSourced>
        where TEventSourced : IEventSourced
    {
        private readonly IEventStore _eventStore;
        private readonly IEventSourcedFactory<TEventSourced> _eventSourcedFactory;

        public EventSourcedRepository(IEventStore eventStore, IEventSourcedFactory<TEventSourced> eventSourcedFactory)
        {
            if (eventStore == null) throw new ArgumentNullException(nameof(eventStore));
            if (eventSourcedFactory == null) throw new ArgumentNullException(nameof(eventSourcedFactory));

            _eventStore = eventStore;
            _eventSourcedFactory = eventSourcedFactory;
        }

        public IEventSourced Find(Guid sourceId)
        {
            var sourcedEvents = _eventStore.GetById(sourceId).ToArray();
            if (!sourcedEvents.Any()) return null;

            return CreateEventSourced(sourceId, sourcedEvents);
        }

        public void Save(TEventSourced eventSourced)
        {
            if (eventSourced == null) throw new ArgumentNullException(nameof(eventSourced));

            var events = eventSourced.Events.ToArray();
            if (!events.Any()) return;

            _eventStore.Save(events);
        }

        protected virtual TEventSourced CreateEventSourced(Guid id, IEnumerable<IEvent> history)
            => _eventSourcedFactory.Create(id, history);
    }
}