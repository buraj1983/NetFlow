using System;
using NetFlow.Infrastructure.Queries;
using NetFlow.Infrastructure.Queries.Handling;

namespace NetFlow.Queries
{
    public delegate IRequestHandler CreateRequestHandler(Type requestType, Type resultType);
}