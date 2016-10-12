using System.Collections.Generic;
using NetFlow.Infrastructure.Queries;
using NetFlow.Queries.Security.Dto;

namespace NetFlow.Queries.Security.Queries
{
    public class GetUsers : IQuery<IEnumerable<UserDto>>
    {
    }
}