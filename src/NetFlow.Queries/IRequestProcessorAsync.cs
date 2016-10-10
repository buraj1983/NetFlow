using System.Threading.Tasks;
using NetFlow.Queries.Requests;

namespace NetFlow.Queries
{
    public interface IRequestProcessorAsync
    {
        Task<TResult> ProcessAsync<TRequset, TResult>(TRequset request) where TRequset : IDataRequest<TResult>;
    }
}