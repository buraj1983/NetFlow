namespace NetFlow.Common.Messaging
{
    public interface IBus
    {
        void RegisterSaga<T>();

        void RegisterHandler<T>();

        void Send<T>(T message);

        void RaiseEvent<T>(T theEvent);
    }
}
