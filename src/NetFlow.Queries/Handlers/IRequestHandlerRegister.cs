using NetFlow.Queries.Requests;

namespace NetFlow.Queries.Handlers
{
    public interface IRequestHandlerRegister
    {
        void Register<TRequestHandler, TRequest, TResult>()
            where TRequestHandler : IRequestHandler<TRequest, TResult>
            where TRequest : IDataRequest<TResult>;
    }
}