using System.Threading.Tasks;

namespace NetFlow.Queries
{
    public interface IRequestProcessorAsync
    {
        Task<TResult> ProcessAsync<TRequest, TResult>(TRequest request) where TRequest : IDataRequest<TResult>;
    }
}