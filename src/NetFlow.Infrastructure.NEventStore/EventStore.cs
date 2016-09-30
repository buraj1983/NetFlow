using System;
using System.Collections.Generic;
using System.Linq;
using NetFlow.Common.Messaging;
using NetFlow.Domain.Persistence;
using NetFlow.Infrastructure.NEventStore.Extensions;
using NEventStore;

namespace NetFlow.Infrastructure.NEventStore
{
    public class EventStore : IEventStore, IDisposable
    {
        private IStoreEvents _storeEvents;
        private bool _isDisposed;

        public EventStore(Wireup config)
        {
            if (config == null) throw new ArgumentNullException(nameof(config));
            _storeEvents = config.Build();
        }

        public IEnumerable<IDomainEvent> GetEvents(Guid streamId)
        {
            using (var stream = _storeEvents.OpenStream(streamId, 0))
            {
                return stream.CommittedEvents.Select(m => m.Body).OfType<IDomainEvent>();
            }
        }

        public IEnumerable<Guid> Save(IEnumerable<IDomainEvent> events)
            => events.GroupByAggregate().Select(SaveToStream);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed) return;

            if (disposing)
                _storeEvents?.Dispose();

            _storeEvents = null;
            _isDisposed = true;
        }

        private Guid SaveToStream(IGrouping<Guid, IDomainEvent> aggregateEvents)
        {
            using (var stream = _storeEvents.OpenStream(aggregateEvents.Key, 0))
            {
                foreach (var @event in aggregateEvents)
                    stream.Add(@event.ToEventMessage());
                
                stream.CommitChanges(Guid.NewGuid());
                return aggregateEvents.Key;
            }
        }
    }
}
