using System;
using NetFlow.Domain.Security.Commands;
using NetFlow.Domain.Security.Events;
using NetFlow.Infrastructure.EventSourcing;
using NetFlow.Infrastructure.Messaging.Handling;

namespace NetFlow.Domain.Security.Handlers
{
    public class AccountRegistration : ICommandHandler<RegisterAccount>, IEventHandler<AccountRegistered>
    {
        private readonly IEventSourcedRepository<Account> _accountRepository;

        public AccountRegistration(IEventSourcedRepository<Account> accountRepository)
        {
            if (accountRepository == null) throw new ArgumentNullException(nameof(accountRepository));
            _accountRepository = accountRepository;
        }

        public void Handle(RegisterAccount command)
        {
            var account = _accountRepository.Find(command.Id);
        }

        public void Handle(AccountRegistered @event)
        {
            
        }
    }
}
