using System;
using NetFlow.Infrastructure.Messaging;

namespace NetFlow.Domain.Security.Events
{
    public class AccountRegistered : IEvent
    {
        public Guid SourceId { get; }

        public AccountRegistered(Guid accountId)
        {
            SourceId = accountId;
        }
    }
}
