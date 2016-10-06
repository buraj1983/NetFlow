using NetFlow.Infrastructure.EventSourcing.NEventStore.Sql;
using NEventStore;

namespace NetFlow.Infrastructure.EventSourcing.NEventStore
{
    public static class NStoreBuilderExtension
    {
        public static SqlPersistenceNStoreBuilder UseSqlPersistence(this NStoreBuilder builder, string connectionName)
            => new SqlPersistenceNStoreBuilder(builder.Config.UsingSqlPersistence(connectionName));
    }
}