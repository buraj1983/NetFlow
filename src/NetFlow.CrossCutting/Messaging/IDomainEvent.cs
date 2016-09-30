using System;

namespace NetFlow.Common.Messaging
{
    public interface IDomainEvent : IMessage
    {
        Guid AggregateId { get; }
    }
}