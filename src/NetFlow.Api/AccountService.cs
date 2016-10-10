using System;
using NetFlow.Domain.Security.Commands;
using NetFlow.Infrastructure.Messaging;
using NetFlow.Queries;
using NetFlow.Queries.Security.Dto;
using NetFlow.Queries.Security.Requests;


namespace NetFlow.Api
{
    public class AccountService : IAccountService
    {
        private readonly ICommandDispatcher _commandBus;
        private readonly IRequestProcessor _requests;

        public AccountService(ICommandDispatcher commandBus, IRequestProcessor requests)
        {
            if (commandBus == null) throw new ArgumentNullException(nameof(commandBus));
            
            _commandBus = commandBus;
            _requests = requests;
        }

        public void Register(string username, string password, string firstName, string lastName, string email)
        {
            _commandBus.Dispatch(new RegisterUser(username, password, firstName, lastName, email));
        }

        public UserDto FindUsersByLogin(string login)
        {
            return _requests.Process<FindUserByLogin, UserDto>(new FindUserByLogin { Login = login });
        }
    }
}
