using System;
using System.Collections.Generic;
using System.Linq;
using NetFlow.Infrastructure.Messaging;
using NEventStore;

namespace NetFlow.Infrastructure.EventSourcing.NEventStore
{
    public sealed class NStore : IEventStore
    {
        private readonly IStoreEvents _storeEvents;

        public NStore(IStoreEvents storeEvents)
        {
            if (storeEvents == null) throw new ArgumentNullException(nameof(storeEvents));
            _storeEvents = storeEvents;
        }

        void IEventStore.Save(IEnumerable<IEvent> events)
        {
            if (events == null) throw new ArgumentNullException(nameof(events));

            foreach (var eventsBySourceId in events.GroupBy(e => e.SourceId))
                SaveToStream(eventsBySourceId.Key, eventsBySourceId);
        }

        IEnumerable<IEvent> IEventStore.GetById(Guid sourceId)
        {
            using (var stream = _storeEvents.OpenStream(sourceId, 0))
                return stream.CommittedEvents.Select(message => message.Body).OfType<IEvent>();
        }

        public void Dispose()
        {
            _storeEvents.Dispose();
        }

        private void SaveToStream(Guid streamId, IEnumerable<IEvent> events)
        {
            using (var stream = _storeEvents.OpenStream(streamId, 0))
            {
                foreach (var @event in events)
                    stream.Add(ToMessage(@event));

                stream.CommitChanges(Guid.NewGuid());
            }
        }

        private static EventMessage ToMessage(IEvent @event)
        {
            return new EventMessage
            {
                Body = @event,
                Headers = new Dictionary<string, object> { { "Type", @event.GetType() } }
            };
        }
    }
}