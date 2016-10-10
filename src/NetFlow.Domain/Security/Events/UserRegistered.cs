using System;
using NetFlow.Infrastructure.Messaging;

namespace NetFlow.Domain.Security.Events
{
    public class UserRegistered : IEvent
    {
        public Guid SourceId { get; }

        public UserRegistered(Guid accountId)
        {
            SourceId = accountId;
        }
    }
}
