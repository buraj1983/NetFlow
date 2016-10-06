using System;
using System.Linq;
using System.Reflection;
using NetFlow.Infrastructure.Messaging.Handling;
using System.Collections.Generic;

namespace NetFlow.Infrastructure.Extensions
{
    public static class CommandHandlerRegisterExtension
    {
        public static TRegister RegisterCommandHandlerFromAssemblies<TRegister>(this TRegister register,
            params Assembly[] assemblies)
            where TRegister : ICommandHandlerRegister
        {
            var handlerGenericType = typeof(ICommandHandler<>);
            var registerMethod = register.GetType().GetMethod("Register");

            var handlerInfos =
                assemblies.SelectMany(a => a.GetTypes())
                    .Where(t => t.IsHandler(handlerGenericType))
                    .Select(t => t.CreateHandlerInfo(handlerGenericType));

            foreach (var handlerInfo in handlerInfos)
            {
                foreach (var commandType in handlerInfo.HandleTypes)
                {
                    registerMethod.MakeGenericMethod(commandType, handlerInfo.DeclaringType)?.Invoke(register, null);
                }
            }

            return register;
        }
    }
}