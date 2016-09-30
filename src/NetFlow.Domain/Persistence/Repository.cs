using System;
using System.Collections.Generic;
using System.Linq;
using NetFlow.Common.Messaging;
using NetFlow.Domain.Extensions;
using NetFlow.Domain.Models;

namespace NetFlow.Domain.Persistence
{
    public class Repository<TAggregate> : IRepository<TAggregate> where TAggregate: IAggregate
    {
        private readonly IAggregateFactory<TAggregate> _aggregateFactory;
        private readonly IEventStore _eventStore;

        public Repository(IAggregateFactory<TAggregate> aggregateFactory, IEventStore eventStore)
        {
            if (aggregateFactory == null) throw new ArgumentNullException(nameof(aggregateFactory));
            if (eventStore == null) throw new ArgumentNullException(nameof(eventStore));

            _aggregateFactory = aggregateFactory;
            _eventStore = eventStore;
        }

        public void Save(TAggregate aggregate)
        {
            if (aggregate == null) throw new ArgumentNullException(nameof(aggregate));
            if (!aggregate.HasUncommitedEvents()) return;

            SaveEvents(aggregate.GetUncommittedEvents());
            aggregate.ClearUncommittedEvents();
        }
        
        public TAggregate GetById(Guid id)
        {
            var restoredEvent = _eventStore.GetEvents(id).ToArray();
            if (!restoredEvent.Any()) return default(TAggregate);

            var aggregate = _aggregateFactory.Create(id);
            aggregate.ReplayEvents(restoredEvent);
            
            return aggregate;
        }

        private void SaveEvents(IEnumerable<IDomainEvent> events) => _eventStore.Save(events);
    }
}