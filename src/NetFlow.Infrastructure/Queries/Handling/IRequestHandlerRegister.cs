namespace NetFlow.Infrastructure.Queries.Handling
{
    public interface IRequestHandlerRegister
    {
        void Register<TRequestHandler, TRequest, TResult>()
            where TRequestHandler : IRequestHandler<TRequest, TResult>
            where TRequest : IDataRequest<TResult>;
    }
}