namespace NetFlow.Common.Messaging
{
    public interface ICommand : IMessage
    {
        string Name { get; }
    }
}