using System;
using NetFlow.Domain.Security.Commands;
using NetFlow.Infrastructure.Messaging;
using NetFlow.Infrastructure.Queries;
using NetFlow.Queries;
using NetFlow.Queries.Security.Dto;
using NetFlow.Queries.Security.Queries;


namespace NetFlow.Api
{
    public class AccountService : IAccountService
    {
        private readonly ICommandDispatcher _commandBus;
        private readonly IQueryProcessor _queries;

        public AccountService(ICommandDispatcher commandBus, IQueryProcessor queries)
        {
            if (commandBus == null) throw new ArgumentNullException(nameof(commandBus));
            
            _commandBus = commandBus;
            _queries = queries;
        }

        public void Register(string username, string password, string firstName, string lastName, string email)
        {
            _commandBus.Dispatch(new RegisterUser(username, password, firstName, lastName, email));
        }

        public UserDto FindUsersByLogin(string login)
        {
            return _queries.Process<FindUserByLogin, UserDto>(new FindUserByLogin { Login = login });
        }
    }
}
