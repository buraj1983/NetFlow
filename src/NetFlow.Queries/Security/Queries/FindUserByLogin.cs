using NetFlow.Infrastructure.Queries;
using NetFlow.Queries.Security.Dto;

namespace NetFlow.Queries.Security.Queries
{
    public class FindUserByLogin : IQuery<UserDto>
    {
        public string Login { get; set; }
    }
}
