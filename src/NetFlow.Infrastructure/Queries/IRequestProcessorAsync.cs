using System.Threading.Tasks;

namespace NetFlow.Infrastructure.Queries
{
    public interface IRequestProcessorAsync
    {
        Task<TResult> ProcessAsync<TRequest, TResult>(TRequest request) where TRequest : IDataRequest<TResult>;
    }
}