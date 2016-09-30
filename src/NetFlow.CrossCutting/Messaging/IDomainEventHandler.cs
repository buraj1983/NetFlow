namespace NetFlow.Common.Messaging
{
    public interface IDomainEventHandler<TEvent> where TEvent : IDomainEvent
    {
        void HandleEvent(TEvent @event);
    }
}
