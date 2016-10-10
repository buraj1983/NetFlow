namespace NetFlow.Infrastructure.Queries.Handling
{
    public interface IRequestHandler { }

    public interface IRequestHandler<in TRequest, out TResult> : IRequestHandler where TRequest : IDataRequest<TResult>
    {
        TResult Handle(TRequest request);
    }
}