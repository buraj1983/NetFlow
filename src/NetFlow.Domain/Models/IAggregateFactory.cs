using System;

namespace NetFlow.Domain.Models
{
    public interface IAggregateFactory<TAggregate> where TAggregate : IAggregate
    {
        TAggregate Create(Guid id);
    }
}
