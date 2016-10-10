using NetFlow.Queries.Security.Dto;

namespace NetFlow.Queries.Security.Requests
{
    public class FindUserByLogin : IDataRequest<UserDto>
    {
        public string Login { get; set; }
    }
}
