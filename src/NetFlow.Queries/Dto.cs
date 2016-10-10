using System;
using NetFlow.Infrastructure.Database;

namespace NetFlow.Queries
{
    public class Dto : IEntity<Guid>
    {
        public Guid Id { get; set; }
    }
}
