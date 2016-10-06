using System;
using NEventStore;

namespace NetFlow.Infrastructure.EventSourcing.NEventStore
{
    public class PersistenceNStoreBuilder : NStoreBuilder
    {
        internal PersistenceNStoreBuilder(PersistenceWireup persistenceConfig) 
            : base(persistenceConfig)
        {
            if (persistenceConfig == null)
                throw new ArgumentNullException(nameof(persistenceConfig));
        }

        public PersistenceNStoreBuilder InitializeStorageEngine()
        {
            PersistenceConfig.InitializeStorageEngine();
            return this;
        }

        internal PersistenceWireup PersistenceConfig => Config as PersistenceWireup;
    }
}