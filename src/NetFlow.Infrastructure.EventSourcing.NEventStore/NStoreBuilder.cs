using System;
using NEventStore;

namespace NetFlow.Infrastructure.EventSourcing.NEventStore
{
    public class NStoreBuilder
    {
        internal NStoreBuilder(Wireup config)
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config));
            Config = config;
        }

        public virtual IEventStore Build() => new NStore(Config.Build());

        internal Wireup Config { get; }

        public static NStoreBuilder Setup() => new NStoreBuilder(Wireup.Init());
    }
}
