using System;
using System.Collections.Generic;
using NetFlow.Infrastructure.Messaging;

namespace NetFlow.Infrastructure.EventSourcing
{
    public class EventSourced : IEventSourced
    {
        private readonly IList<IEvent> _pendingEvents = new List<IEvent>();

        private readonly IDictionary<Type, Action<IEvent>> _handlersByEventType 
            = new Dictionary<Type, Action<IEvent>>();

        protected EventSourced(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }

        public int Version { get; private set; } = -1;

        public IEnumerable<IEvent> Events => _pendingEvents;

        protected void RegisterHandler<TEvent>(Action<TEvent> handler) where TEvent : IEvent
        {
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            var eventType = typeof(TEvent);
            if (_handlersByEventType.ContainsKey(eventType))
                throw new ArgumentException("Event handler is already registered.");

            _handlersByEventType.Add(eventType, (evt) => handler.Invoke((TEvent)evt));
        }

        protected void RiseEvent<TEvent>(TEvent @event) where TEvent : IEvent
        {
            if (@event == null) throw new ArgumentNullException(nameof(@event));

            ApplyEvent(@event);
            _pendingEvents.Add(@event);
        }

        protected void ReplayEvents(IEnumerable<IEvent> events)
        {
            foreach (var @event in events)
            {
                ApplyEvent(@event);
            }
        }

        private void ApplyEvent(IEvent @event)
        {
            if (@event == null) throw new ArgumentNullException(nameof(@event));

            var eventType = @event.GetType();
            Action<IEvent> handler;

            if (!_handlersByEventType.TryGetValue(eventType, out handler))
                throw new ArgumentException(@"Event '{eventType}' is not supported.");

            handler.Invoke(@event);
            IncrementVersion();
        }
        
        private void IncrementVersion() => Version += 1;
    }
}