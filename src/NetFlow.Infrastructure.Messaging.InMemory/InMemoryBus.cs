using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using NetFlow.Infrastructure.Messaging.Handling;

namespace NetFlow.Infrastructure.Messaging.InMemory
{
    public class InMemoryBus : IBus, ICommandDispatcher, IEventDispatcher
    {
        private ConcurrentDictionary<Type, Func<Type, ICommandHandler>> _handlerFactoriesByCommandType;

        public InMemoryBus()
        {
            _handlerFactoriesByCommandType = new ConcurrentDictionary<Type, Func<Type, ICommandHandler>>();
        }

        public void Send<TCommand>(TCommand command) where TCommand : ICommand
        {
        }

        public void Rise<TEvent>(TEvent @event) where TEvent : IEvent
        {
        }

        public void RegisterCommandHandler<TCommandHandler, TCommand>()
            where TCommandHandler : ICommandHandler<TCommand> 
            where TCommand : ICommand
        {
            //if(_handlerFactoriesByCommandType.TryAdd(typeof(TCommand),))
        }

        public void RegisterEventHandler<TEventHandler, TEvent>() 
            where TEventHandler : IEventHandler<TEvent>
            where TEvent : IEvent
        {
            throw new System.NotImplementedException();
        }

        void ICommandDispatcher.Dispatch<TCommand>(TCommand command)
        {
            throw new System.NotImplementedException();
        }

        void IEventDispatcher.Dispatch<TEvent>(TEvent @event)
        {
            throw new System.NotImplementedException();
        }
    }
}
