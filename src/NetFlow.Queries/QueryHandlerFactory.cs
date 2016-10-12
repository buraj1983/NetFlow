using System;
using NetFlow.Infrastructure.Queries;

namespace NetFlow.Queries
{
    public delegate IQueryHandler QueryHandlerFactory(Type queryType, Type resultType);
}
