using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using NetFlow.Infrastructure.Messaging.Handling;

namespace NetFlow.Infrastructure.Extensions
{
    public static class TypeExtension
    {
        public static bool IsClosedType(this Type target, Type openTypeDefinition)
        {
            ThrowIfNotOpenType(openTypeDefinition);
            return target.IsConstructedGenericType && target.GetGenericTypeDefinition() == openTypeDefinition;
        }

        public static IEnumerable<Type> GetClosedTypeInterfaces(this Type target, Type openTypeDefinition)
            => target.GetInterfaces().Where(t => t.IsClosedType(openTypeDefinition));
        
        private static void ThrowIfNotOpenType(Type target)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));

            if (!target.IsGenericTypeDefinition)
                throw new ArgumentException("Type is not generic type definition.", nameof(target));
        }
    }
}