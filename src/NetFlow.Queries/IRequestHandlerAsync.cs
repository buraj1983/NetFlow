using System.Threading.Tasks;

namespace NetFlow.Queries
{
    public interface IRequestHandlerAsync<in TRequest, TResult> where TRequest : IDataRequest<TResult>
    {
        Task<TResult> HandleAsync(TRequest request);
    }
}