using System.Threading.Tasks;

namespace NetFlow.Infrastructure.Queries.Handling
{
    public interface IRequestHandlerAsync { }

    public interface IRequestHandlerAsync<in TRequest, TResult> : IRequestHandlerAsync where TRequest : IDataRequest<TResult>
    {
        Task<TResult> HandleAsync(TRequest request);
    }
}