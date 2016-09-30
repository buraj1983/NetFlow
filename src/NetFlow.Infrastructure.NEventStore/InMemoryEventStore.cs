using NEventStore;

namespace NetFlow.Infrastructure.NEventStore
{
    public class InMemoryEventStore : EventStore
    {
        public InMemoryEventStore() : base(Configuration())
        {
        }

        private static Wireup Configuration()
        {
            return Wireup.Init().UsingInMemoryPersistence().UsingJsonSerialization();
        }
    }
}