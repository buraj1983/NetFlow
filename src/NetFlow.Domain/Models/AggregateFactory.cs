using System;

namespace NetFlow.Domain.Models
{
    public class AggregateFactory<TAggregate> : IAggregateFactory<TAggregate> where TAggregate : IAggregate
    {
        public virtual TAggregate Create(Guid id) 
            => (TAggregate) Activator.CreateInstance(typeof(TAggregate), id);
    }
}