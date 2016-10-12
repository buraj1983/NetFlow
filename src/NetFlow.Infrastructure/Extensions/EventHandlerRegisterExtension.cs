using System.Linq;
using System.Reflection;
using NetFlow.Infrastructure.Messaging.Handling;

namespace NetFlow.Infrastructure.Extensions
{
    public static class EventHandlerRegisterExtension
    {
        //public static TRegister RegisterEventHandlerFromAssemblies<TRegister>(this TRegister register,
        //    params Assembly[] assemblies)
        //    where TRegister : IEventHandlerRegister
        //{
        //    var handlerGenericType = typeof(IEventHandler<>);
        //    var registerMethod = register.GetType().GetMethod("Register");

        //    var handlerInfos =
        //        assemblies.SelectMany(a => a.GetTypes())
        //            .Where(t => t.IsHandler(handlerGenericType))
        //            .Select(t => t.CreateHandlerInfo(handlerGenericType));

        //    foreach (var handlerInfo in handlerInfos)
        //    {
        //        foreach (var eventType in handlerInfo.HandleTypes)
        //        {
        //            registerMethod.MakeGenericMethod(eventType, handlerInfo.DeclaringType)?.Invoke(register, null);
        //        }
        //    }

        //    return register;
        //}
    }
}