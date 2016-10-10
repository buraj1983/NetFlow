using System;
using System.Linq;

namespace NetFlow.Infrastructure.Database.SqlQueries
{
    public class QuerySqlContext : IDbContext, IReadOnlyDbContext
    {
        IQueryable<TEntity> IDbContext.Get<TEntity, TKey>()
        {
            throw new NotImplementedException();
        }

        int IDbContext.SaveChanges()
        {
            throw new NotImplementedException();
        }

        IQueryable<TEntity> IReadOnlyDbContext.Get<TEntity, TKey>()
        {
            throw new NotImplementedException();
        }
    }
}
