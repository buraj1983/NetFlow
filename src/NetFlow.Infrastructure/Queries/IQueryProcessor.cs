namespace NetFlow.Infrastructure.Queries
{
    public interface IQueryProcessor
    {
        TResult Process<TQuery, TResult>(TQuery query) 
            where TQuery : IQuery<TResult>;

        void RegisterHandler<TQueryHandler, TQuery, TResult>() 
            where TQueryHandler : IQueryHandler<TQuery, TResult>
            where TQuery : IQuery<TResult>;
    }
}