using System;
using NetFlow.Domain.Security.Commands;
using NetFlow.Infrastructure.Messaging;

namespace NetFlow.Api
{
    public class AccountService : IAccountService
    {
        private readonly ICommandDispatcher _commandDispatcher;
        
        public AccountService(ICommandDispatcher commandDispatcher)
        {
            if (commandDispatcher == null) throw new ArgumentNullException(nameof(commandDispatcher));
            
            _commandDispatcher = commandDispatcher;
        }

        public void Register(string username, string password, string firstName, string lastName, string email)
        {
            _commandDispatcher.Dispatch(new RegisterAccount(username, password, firstName, lastName, email));
        }
    }
}
