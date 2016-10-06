namespace NetFlow.Infrastructure.Messaging.Handling
{
    public interface ICommandHandlerRegister
    {
        void Register<TCommand, TCommandHandler>()
            where TCommand : ICommand
            where TCommandHandler : ICommandHandler<TCommand>;
    }
}