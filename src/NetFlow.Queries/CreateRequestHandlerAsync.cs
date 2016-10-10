using System;
using NetFlow.Infrastructure.Queries;
using NetFlow.Infrastructure.Queries.Handling;

namespace NetFlow.Queries
{
    public delegate IRequestHandlerAsync CreateRequestHandlerAsync(Type requestType, Type resultType);
}