using System;
using NetFlow.Infrastructure.Messaging.Handling;

namespace NetFlow.Infrastructure.Messaging.InMemory
{
    public delegate ICommandHandler CommandHandlerFactory(Type commandType);
}