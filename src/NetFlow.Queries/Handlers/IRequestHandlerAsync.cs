using System.Threading.Tasks;
using NetFlow.Queries.Requests;

namespace NetFlow.Queries.Handlers
{
    public interface IRequestHandlerAsync<in TRequest, TResult> where TRequest : IDataRequest<TResult>
    {
        Task<TResult> HandleAsync(TRequest request);
    }
}