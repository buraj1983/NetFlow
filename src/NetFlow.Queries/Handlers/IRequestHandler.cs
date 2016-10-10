using NetFlow.Queries.Requests;

namespace NetFlow.Queries.Handlers
{
    public interface IRequestHandler<in TRequest, out TResult> where TRequest : IDataRequest<TResult>
    {
        TResult Handle(TRequest request);
    }
}