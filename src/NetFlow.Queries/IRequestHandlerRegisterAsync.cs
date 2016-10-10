namespace NetFlow.Queries
{
    public interface IRequestHandlerRegisterAsync
    {
        void Register<TRequestHandlerAsync, TRequest, TResult>()
            where TRequestHandlerAsync : IRequestHandlerAsync<TRequest, TResult>
            where TRequest : IDataRequest<TResult>;
    }
}