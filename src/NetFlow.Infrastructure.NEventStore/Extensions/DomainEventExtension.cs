using System.Collections.Generic;
using NetFlow.Common.Messaging;
using NEventStore;

namespace NetFlow.Infrastructure.NEventStore.Extensions
{
    internal static class DomainEventExtension
    {
        internal static EventMessage ToEventMessage(this IDomainEvent @event)
        {
            return new EventMessage
            {
                Body = @event,
                Headers = @event.CreateEventHeaders()
            };
        }

        internal static Dictionary<string, object> CreateEventHeaders(this IDomainEvent @event)
        {
            return new Dictionary<string, object>
            {
                { "Type", @event.GetType() }
            };
        }
    }
}