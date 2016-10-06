namespace NetFlow.Infrastructure.Messaging
{
    public interface IEventDispatcher
    {
        void Dispatch<TEvent>(TEvent @event) where TEvent : IEvent;
    }
}