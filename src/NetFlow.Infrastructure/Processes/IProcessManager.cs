using System;
using System.Collections.Generic;
using NetFlow.Infrastructure.Messaging;

namespace NetFlow.Infrastructure.Processes
{
    public interface IProcessManager
    {
        Guid Id { get; }

        bool IsCompleted { get; }

        IEnumerable<ICommand> Commands { get; }
    }
}
