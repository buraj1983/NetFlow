using System;
using System.Collections.Generic;
using System.Linq;
using NetFlow.Common.Messaging;

namespace NetFlow.Infrastructure.NEventStore.Extensions
{
    internal static class DomainEventEnumerableExtension
    {
        public static IEnumerable<IGrouping<Guid, IDomainEvent>> GroupByAggregate(this IEnumerable<IDomainEvent> events)
            => events.GroupBy(x => x.AggregateId);
    }
}
