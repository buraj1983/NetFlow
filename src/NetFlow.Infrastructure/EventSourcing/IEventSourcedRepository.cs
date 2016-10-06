using System;

namespace NetFlow.Infrastructure.EventSourcing
{
    public interface IEventSourcedRepository<TEventSourced> where TEventSourced : IEventSourced
    {
        IEventSourced Find(Guid sourceId);

        void Save(TEventSourced eventSourced);
    }
}