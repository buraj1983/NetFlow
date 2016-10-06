using System;
using System.Linq;
using NetFlow.Infrastructure.Messaging.Handling;

namespace NetFlow.Infrastructure.Extensions
{
    public static class TypeExtension
    {
        public static bool IsAssignableToGenericType(this Type target, Type genericTypeDefinition)
        {
            if (target.IsGenericType(genericTypeDefinition) ||
                target.GetInterfaces().Any(i => IsGenericType(i, genericTypeDefinition))) return true;
            
            return target.BaseType != null && IsAssignableToGenericType(target.BaseType, genericTypeDefinition);
        }

        public static bool IsGenericType(this Type target, Type genericTypeDefinition)
        {
            if (genericTypeDefinition == null)
                throw new ArgumentNullException(nameof(genericTypeDefinition));
            if (!genericTypeDefinition.IsGenericTypeDefinition)
                throw new ArgumentException("Argument is not generic type definition.");

            return target.IsGenericType && target.GetGenericTypeDefinition() == genericTypeDefinition;
        }

        public static bool IsHandler(this Type target, Type handlerGenericType)
        {
            if (handlerGenericType == null) throw new ArgumentNullException(nameof(handlerGenericType));

            return target.IsClass && target.IsAssignableToGenericType(handlerGenericType);
        }
        
        public static IHandlerInfo CreateHandlerInfo(this Type target, Type handlerGenericType)
            => new HandlerInfo(target, handlerGenericType);
        
    }
}