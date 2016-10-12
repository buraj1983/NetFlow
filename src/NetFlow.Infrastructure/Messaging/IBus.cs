using NetFlow.Infrastructure.Messaging.Handling;

namespace NetFlow.Infrastructure.Messaging
{
    public interface IBus
    {
        void Send<TCommand>(TCommand command) where TCommand : ICommand;

        void Rise<TEvent>(TEvent @event) where TEvent : IEvent;

        void RegisterCommandHandler<TCommandHandler, TCommand>() 
            where TCommandHandler : ICommandHandler<TCommand>
            where TCommand : ICommand;

        void RegisterEventHandler<TEventHandler, TEvent>()
            where TEventHandler : IEventHandler<TEvent>
            where TEvent : IEvent;
    }
}
