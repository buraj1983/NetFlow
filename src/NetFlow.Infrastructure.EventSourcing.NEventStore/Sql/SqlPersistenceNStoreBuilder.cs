using System;
using NEventStore;
using NEventStore.Persistence.Sql.SqlDialects;

namespace NetFlow.Infrastructure.EventSourcing.NEventStore.Sql
{
    public class SqlPersistenceNStoreBuilder : PersistenceNStoreBuilder
    {
        internal SqlPersistenceNStoreBuilder(SqlPersistenceWireup sqlPersistenceConfig) 
            : base(sqlPersistenceConfig)
        {
            if (sqlPersistenceConfig == null)
                throw new ArgumentNullException(nameof(sqlPersistenceConfig));
        }

        public SqlPersistenceNStoreBuilder UseDialect(SqlDialect dialect)
        {
            switch (dialect)
            {
                case SqlDialect.MsSql:
                    SqlConfig.WithDialect(new MsSqlDialect());
                    break;
                case SqlDialect.MySql:
                    SqlConfig.WithDialect(new MySqlDialect());
                    break;
                case SqlDialect.Postgre:
                    SqlConfig.WithDialect(new PostgreSqlDialect());
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dialect), dialect, null);
            }
            return this;
        }

        internal SqlPersistenceWireup SqlConfig => PersistenceConfig as SqlPersistenceWireup;   
    }
}