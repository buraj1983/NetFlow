namespace NetFlow.Api
{
    public interface IAccountService : IService
    {
        void Register(string username, string password, string firstName, string lastName, string email);
    }
}