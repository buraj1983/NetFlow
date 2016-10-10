using System;
using System.Collections.Generic;
using NetFlow.Infrastructure.EventSourcing;
using NetFlow.Infrastructure.Messaging;

namespace NetFlow.Domain.Security
{
    public class User : EventSourced
    {
        public User(Guid id) 
            : base(id)
        {
        }

        public User(Guid id, IEnumerable<IEvent> commitedEvents) 
            : base(id)
        {
            ReplayEvents(commitedEvents);
        }
    }
}
