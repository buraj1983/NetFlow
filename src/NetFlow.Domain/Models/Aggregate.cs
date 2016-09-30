using System;
using System.Collections.Generic;
using System.Linq;
using NetFlow.Common.Messaging;
using NetFlow.Domain.Extensions;

namespace NetFlow.Domain.Models
{
    public abstract class Aggregate : IAggregate
    {
        private readonly IList<IDomainEvent> _uncommittedEvents;

        public Guid Id { get; }

        protected Aggregate(Guid id)
        {
            Id = id;
            _uncommittedEvents = new List<IDomainEvent>();
        }
        
        public IEnumerable<IDomainEvent> GetUncommittedEvents() => _uncommittedEvents.ToArray();

        public void ClearUncommittedEvents() => _uncommittedEvents.Clear();

        public void ApplyEvent(IDomainEvent @event)
        {
            if (@event == null) throw new ArgumentNullException(nameof(@event));

            var eventType = @event.GetType();
            var handlerMethod = this.FindHandlerMethodForEvent(eventType);

            if (handlerMethod == null)
                throw new ArgumentException($"Could not handle event type '{eventType}'.", nameof(@event));

            handlerMethod.Invoke(this, new object[] { @event });
        }

        protected void RiseEvent<TEvent>(TEvent @event) where TEvent : IDomainEvent
        {
            ApplyEvent(@event);
            _uncommittedEvents.Add(@event);
        }
    }
}