using System;
using NetFlow.Domain.Security.Commands;
using NetFlow.Domain.Security.Events;
using NetFlow.Infrastructure.EventSourcing;
using NetFlow.Infrastructure.Messaging.Handling;

namespace NetFlow.Domain.Security.Handlers
{
    public class UserRegistration : ICommandHandler<RegisterUser>, IEventHandler<UserRegistered>
    {
        private readonly IEventSourcedRepository<User> _accountRepository;

        public UserRegistration(IEventSourcedRepository<User> accountRepository)
        {
            if (accountRepository == null) throw new ArgumentNullException(nameof(accountRepository));
            _accountRepository = accountRepository;
        }

        public void Handle(RegisterUser command)
        {
            var account = _accountRepository.Find(command.Id);
        }

        public void Handle(UserRegistered @event)
        {
            
        }
    }
}
