using System.Linq;

namespace NetFlow.Infrastructure.Database
{
    public interface IDbContext
    {
        IQueryable<TEntity> Get<TEntity, TKey>() where TEntity : IEntity<TKey>;

        int SaveChanges();
    }
}