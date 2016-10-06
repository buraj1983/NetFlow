using System;

namespace NetFlow.Infrastructure.Messaging
{
    public class Command : ICommand
    {
        public Guid Id { get; }

        protected Command(Guid id)
        {
            Id = id;
        }

        protected Command()
            : this(Guid.NewGuid())
        {
            
        }
    }
}