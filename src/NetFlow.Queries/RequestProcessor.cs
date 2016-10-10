using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using NetFlow.Infrastructure.Exceptions.Handlers;
using NetFlow.Queries.Handlers;
using NetFlow.Queries.Requests;

namespace NetFlow.Queries
{
    public class RequestProcessor : IRequestProcessor, IRequestProcessorAsync, IRequestHandlerRegister,
        IRequestHandlerRegisterAsync
    {
        private ConcurrentDictionary<Type, Type> _registeredHandlersByRequestType = new ConcurrentDictionary<Type, Type>();

        public RequestProcessor()
        {
            
        }

        TResult IRequestProcessor.Process<TRequest, TResult>(TRequest request)
        {
            throw new NotImplementedException();
        }

        Task<TResult> IRequestProcessorAsync.ProcessAsync<TRequset, TResult>(TRequset request)
        {
            throw new NotImplementedException();
        }

        void IRequestHandlerRegister.Register<TRequestHandler, TRequest, TResult>()
        {
            var key = typeof(TRequest);
            var handlerType = typeof(TRequestHandler);
        }

        void IRequestHandlerRegisterAsync.Register<TRequestHandlerAsync, TRequest, TResult>()
        {
            throw new NotImplementedException();
        }

        private Type CreateKey<TRequest, TResult>() where TRequest : IDataRequest<TResult> 
            => typeof(TRequest);
    }
}
