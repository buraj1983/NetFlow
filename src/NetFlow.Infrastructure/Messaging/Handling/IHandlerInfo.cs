using System;
using System.Collections.Generic;

namespace NetFlow.Infrastructure.Messaging.Handling
{
    public interface IHandlerInfo
    {
        Type DeclaringType { get; }

        Type HandlerGenericType { get; }

        IEnumerable<Type> HandleTypes { get; }
    }
}