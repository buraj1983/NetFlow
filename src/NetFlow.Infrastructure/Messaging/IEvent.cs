using System;

namespace NetFlow.Infrastructure.Messaging
{
    public interface IEvent
    {
        Guid SourceId { get; }
    }
}
