using System;
using NetFlow.Domain.Models;

namespace NetFlow.Domain.Persistence
{
    public interface IRepository<TAggregate> where TAggregate : IAggregate
    {
        void Save(TAggregate aggregate);

        TAggregate GetById(Guid id);
    }
}
