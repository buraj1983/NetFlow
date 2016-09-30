using System;

namespace NetFlow.Common.Messaging
{
    public class DomainEvent : Message, IDomainEvent
    {
        public Guid AggregateId { get; }

        protected DomainEvent(Guid aggregateId)
        {
            AggregateId = aggregateId;
        }
    }
}