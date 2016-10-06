using System;
using System.Collections.Concurrent;
using NetFlow.Infrastructure.Messaging.Handling;

namespace NetFlow.Infrastructure.Messaging.InMemory
{
    public class CommandDispatcher : ICommandDispatcher, ICommandHandlerRegister
    {
        private readonly CommandHandlerFactory _handlerFactory;

        private readonly ConcurrentDictionary<Type, Type> _handlersByCommandType =
            new ConcurrentDictionary<Type, Type>();

        public CommandDispatcher(CommandHandlerFactory handlerFactory)
        {
            if (handlerFactory == null) throw new ArgumentNullException(nameof(handlerFactory));
            _handlerFactory = handlerFactory;
        }

        public void Dispatch<TCommand>(TCommand command) where TCommand : ICommand
        {
            if (command == null) throw new ArgumentNullException(nameof(command));

            Type handlerType;
            var commandType = command.GetType();

            if (!_handlersByCommandType.TryGetValue(commandType, out handlerType)) return;

            var commandHandler = _handlerFactory.Invoke(commandType) as ICommandHandler<TCommand>;
            commandHandler?.Handle(command);
        }

        public void Register<TCommand, TCommandHandler>()
            where TCommand : ICommand
            where TCommandHandler : ICommandHandler<TCommand>
        {
            var commandType = typeof(TCommand);

            if (!_handlersByCommandType.ContainsKey(commandType) &&
                _handlersByCommandType.TryAdd(commandType, typeof(TCommandHandler)))
                return;

            throw new ArgumentException($"Command handler for command type '{commandType.Name}' is already registered.");
        }
    }
}
