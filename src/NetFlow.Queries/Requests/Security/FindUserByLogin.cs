using NetFlow.Queries.Dtos.Security;

namespace NetFlow.Queries.Requests.Security
{
    public class FindUserByLogin : IDataRequest<User>
    {
        public string Login { get; set; }
    }
}
