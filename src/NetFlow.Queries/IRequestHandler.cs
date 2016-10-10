namespace NetFlow.Queries
{
    public interface IRequestHandler<in TRequest, out TResult> where TRequest : IDataRequest<TResult>
    {
        TResult Handle(TRequest request);
    }
}