namespace NetFlow.Infrastructure.Messaging.Handling
{
    public interface IEventHandler { }

    public interface IEventHandler<TEvent> : IEventHandler where TEvent : IEvent
    {
        void Handle(TEvent @event);
    }
}