namespace NetFlow.Infrastructure.Queries.Handling
{
    public interface IRequestHandlerRegisterAsync
    {
        void Register<TRequestHandlerAsync, TRequest, TResult>()
            where TRequestHandlerAsync : IRequestHandlerAsync<TRequest, TResult>
            where TRequest : IDataRequest<TResult>;
    }
}