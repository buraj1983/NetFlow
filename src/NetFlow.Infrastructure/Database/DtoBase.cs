namespace NetFlow.Infrastructure.Database
{
    public class DtoBase<TKey> : IEntity<TKey>
    {
        public TKey Id { get; set; }
    }
}
