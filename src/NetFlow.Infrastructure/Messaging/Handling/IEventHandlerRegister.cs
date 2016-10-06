namespace NetFlow.Infrastructure.Messaging.Handling
{
    public interface IEventHandlerRegister
    {
        void Register<TEvent, TEventHandler>() 
            where TEvent : IEvent 
            where TEventHandler : IEventHandler<TEvent>;
    }
}