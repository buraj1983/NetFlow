using System;
using System.Collections.Generic;
using System.Linq;

namespace NetFlow.CrossCutting.Extensions
{
    public static class TypeExtension
    {
        public static IEnumerable<Type> GetGenericInterfaces(this Type target)
            => target.GetInterfaces().Where(t => t.IsGenericType);

        public static IEnumerable<Type> FindGenericInterface(this Type target, Type interfaceGenericTypeDefinition)
        {
            if (interfaceGenericTypeDefinition == null)
                throw new ArgumentNullException(nameof(interfaceGenericTypeDefinition));

            if (!interfaceGenericTypeDefinition.IsInterface || !interfaceGenericTypeDefinition.IsGenericTypeDefinition)
                throw new ArgumentException("Generic type defintion of interface must be provided.",
                    nameof(interfaceGenericTypeDefinition));

            return
                target.GetGenericInterfaces().Where(t => t.GetGenericTypeDefinition() == interfaceGenericTypeDefinition);
        }
    }
}
