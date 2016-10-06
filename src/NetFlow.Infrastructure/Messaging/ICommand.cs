using System;

namespace NetFlow.Infrastructure.Messaging
{
    public interface ICommand
    {
        Guid Id { get; }
    }
}
