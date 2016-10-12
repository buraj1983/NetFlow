using System;
using System.Collections.Generic;
using System.Linq;
using NetFlow.Infrastructure.Extensions;

namespace NetFlow.Infrastructure.Messaging.Handling
{
    //public class HandlerInfo : IHandlerInfo
    //{
    //    private IList<Type> _handleTypes;
        
    //    public HandlerInfo(Type declaringType, Type handlerGenericType)
    //    {
    //        if (declaringType == null)
    //            throw new ArgumentNullException(nameof(declaringType));
    //        if (handlerGenericType == null)
    //            throw new ArgumentNullException(nameof(handlerGenericType));
    //        if (!handlerGenericType.IsInterface || !handlerGenericType.IsGenericType)
    //            throw new ArgumentException("Handler must be interface generic type definition.",
    //                nameof(handlerGenericType));

    //        DeclaringType = declaringType;
    //        HandlerGenericType = handlerGenericType;
    //    }

    //    public Type DeclaringType { get; }

    //    public Type HandlerGenericType { get; }

    //    //public IEnumerable<Type> HandleTypes 
    //    //    => _handleTypes ?? (_handleTypes = FindHandleTypes());

    //    //private IList<Type> FindHandleTypes()
    //    //    =>
    //    //        DeclaringType.GetInterfaces()
    //    //            .Where(i => i.IsGenericType(HandlerGenericType))
    //    //            .Select(t => t.GenericTypeArguments.Single())
    //    //            .ToList();
        
    //}
}