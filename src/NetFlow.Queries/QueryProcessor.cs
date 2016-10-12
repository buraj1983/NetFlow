using System;
using System.Collections.Concurrent;
using NetFlow.Infrastructure.Queries;

namespace NetFlow.Queries
{
    public class QueryProcessor : IQueryProcessor
    {
        private readonly QueryHandlerFactory _handlerFactory;
        private readonly ConcurrentDictionary<Tuple<Type, Type>, Func<IQueryHandler>> _registerdHandlers;

        public QueryProcessor(QueryHandlerFactory handlerFactory)
        {
            if (handlerFactory == null) throw new ArgumentNullException(nameof(handlerFactory));

            _registerdHandlers = new ConcurrentDictionary<Tuple<Type, Type>, Func<IQueryHandler>>();
            _handlerFactory = handlerFactory;
        }

        public TResult Process<TQuery, TResult>(TQuery query) 
            where TQuery : IQuery<TResult>
        {
            Func<IQueryHandler> handlerFactory;
            
            if (!_registerdHandlers.TryGetValue(CreateKey<TQuery, TResult>(), out handlerFactory))
                throw new ArgumentException($"Handler for query '{typeof(TQuery).Name}' is not registered.");

            return ((IQueryHandler<TQuery, TResult>)handlerFactory.Invoke()).Handle(query);
        }

        public void RegisterHandler<TQueryHandler, TQuery, TResult>()
            where TQueryHandler : IQueryHandler<TQuery, TResult>
            where TQuery : IQuery<TResult>
        {
            var key = CreateKey<TQuery, TResult>();

            if (!_registerdHandlers.TryAdd(key, () => _handlerFactory.Invoke(key.Item1, key.Item2)))
                throw new ArgumentException(
                    $"Handler '{typeof(TQueryHandler).Name} for query '{typeof(TQuery).Name}' is already registered.");
        }

        private static Tuple<Type, Type> CreateKey<TFirst, TSecond>()
            => new Tuple<Type, Type>(typeof(TFirst), typeof(TSecond));
    }
}
