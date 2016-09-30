using System;
using System.Collections.Generic;
using NetFlow.Common.Messaging;

namespace NetFlow.Domain.Models
{
    public interface IAggregate
    {
        Guid Id { get; }

        IEnumerable<IDomainEvent> GetUncommittedEvents();

        void ClearUncommittedEvents();

        void ApplyEvent(IDomainEvent @event);
    }
}
