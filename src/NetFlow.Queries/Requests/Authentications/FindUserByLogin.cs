using NetFlow.Queries.Dtos.Authentications;

namespace NetFlow.Queries.Requests.Authentications
{
    public class FindUserByLogin : IDataRequest<User>
    {
        public string Login { get; set; }
    }
}
