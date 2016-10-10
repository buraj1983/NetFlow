using System;
using NetFlow.Infrastructure.Database;

namespace NetFlow.Queries.Dtos
{
    public class Dto : IEntity<Guid>
    {
        public Guid Id { get; set; }
    }
}
