using System.Linq;

namespace NetFlow.Infrastructure.Database
{
    public interface IReadOnlyDbContext
    {
        IQueryable<TEntity> Get<TEntity, TKey>() where TEntity : IEntity<TKey>;
    }
}