using System.Linq;

namespace NetFlow.Infrastructure.Database
{
    public interface IEntity<TKey>
    {
        TKey Id { get; }
    }
}
