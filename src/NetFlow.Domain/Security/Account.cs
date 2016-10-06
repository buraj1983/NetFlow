using System;
using System.Collections.Generic;
using NetFlow.Infrastructure.EventSourcing;
using NetFlow.Infrastructure.Messaging;

namespace NetFlow.Domain.Security
{
    public class Account : EventSourced
    {
        public Account(Guid id) 
            : base(id)
        {
        }

        public Account(Guid id, IEnumerable<IEvent> commitedEvents) 
            : base(id)
        {
            ReplayEvents(commitedEvents);
        }
    }
}
