using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using NetFlow.Infrastructure.Messaging.Handling;

namespace NetFlow.Infrastructure.Messaging.InMemory
{
    public class EventDispatcher : IEventDispatcher, IEventHandlerRegister
    {
        private readonly EventHandlerFactory _handlerFactory;

        private readonly ConcurrentDictionary<Type, IList<Type>> _handlerTypesByEventType =
            new ConcurrentDictionary<Type, IList<Type>>();

        public EventDispatcher(EventHandlerFactory handlerFactory)
        {
            if (handlerFactory == null) throw new ArgumentNullException(nameof(handlerFactory));
            _handlerFactory = handlerFactory;
        }

        public void Dispatch<TEvent>(TEvent @event) where TEvent : IEvent
        {
            if (@event == null) throw new ArgumentNullException(nameof(@event));

            IList<Type> handlers;
            if (!_handlerTypesByEventType.TryGetValue(@event.GetType(), out handlers))
                return;
            
            DispatchToHandlers(@event, 
                handlers.Select(CreateHandler).OfType<IEventHandler<TEvent>>());
        }

        public void Register<TEvent, TEventHandler>()
            where TEvent : IEvent
            where TEventHandler : IEventHandler<TEvent>
        {
            var eventType = typeof(TEvent);

            var handlers = _handlerTypesByEventType.GetOrAdd(eventType, TypeListFactory);
            if(IsHandlerExists<TEvent, TEventHandler>(handlers))
                throw new ArgumentException($"Event handler is alredy registered for event '{eventType}'.");

            handlers.Add(typeof(TEventHandler));
        }

        private IEventHandler CreateHandler(Type eventType)
            => _handlerFactory.Invoke(eventType);

        private static void DispatchToHandlers<TEvent>(TEvent @event, 
            IEnumerable<IEventHandler<TEvent>> handlers)
            where TEvent : IEvent
        {
            foreach (var handler in handlers)
            {
                handler.Handle(@event);
            }
        } 

        private static bool IsHandlerExists<TEvent, TEventHandler>(IEnumerable<Type> handlerTypes)
            where TEvent : IEvent
            where TEventHandler : IEventHandler<TEvent>
        {
            var handlerType = typeof(TEventHandler);
            return handlerTypes
                .Select(t => t.IsInterface && t.IsGenericType && handlerType.IsAssignableFrom(t))
                .Any();
        }

        private static Func<Type, IList<Type>> TypeListFactory => (t) => new List<Type>();
    }
}