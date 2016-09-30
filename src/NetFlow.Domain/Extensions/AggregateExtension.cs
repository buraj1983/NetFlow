using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NetFlow.Common.Messaging;
using NetFlow.CrossCutting.Caching;
using NetFlow.CrossCutting.Extensions;
using NetFlow.Domain.Models;

namespace NetFlow.Domain.Extensions
{
    public static class AggregateExtension
    {
        public static void ReplayEvents(this IAggregate aggregate, IEnumerable<IDomainEvent> events)
        {
            if (events == null || !events.Any()) return;

            foreach(var @event in events)
                aggregate.ApplyEvent(@event);
        }

        /// <summary>
        /// Check if aggregate has uncommited events
        /// </summary>
        /// <param name="aggregate"></param>
        /// <returns></returns>
        public static bool HasUncommitedEvents(this IAggregate aggregate) => aggregate.GetUncommittedEvents().Any();

        /// <summary>
        /// Finds instance IDomainEventHandler<> method implementation
        /// </summary>
        /// <param name="aggregate">Instance type</param>
        /// <param name="eventType">Handler argument type</param>
        /// <returns></returns>
        internal static MethodInfo FindHandlerMethodForEvent(this IAggregate aggregate, Type eventType)
        {
            if (eventType == null) throw new ArgumentNullException(nameof(eventType));
            return FindHandlerMethod.Invoke(CreateTupleKey(aggregate.GetType(), eventType));
        }

        /// <summary>
        /// Finds and caches IDomainEventHandler<> method implementation 
        /// for specified instance type[Typle.Item1] and handler argument type[Typle.Item2]
        /// </summary>
        private static readonly Func<Tuple<Type, Type>, MethodInfo> FindHandlerMethod =
            Memoization.Memoize<Tuple<Type, Type>, MethodInfo>((k) =>
            {
                var method =
                    k.Item1.FindGenericInterface(typeof(IDomainEventHandler<>))
                        .SingleOrDefault(i => i.GetGenericArguments().Single() == k.Item2);
                return method?.GetMethod("HandleEvent");
            });

        private static Tuple<Type, Type> CreateTupleKey(Type instanceType, Type eventType)
            => Tuple.Create(instanceType, eventType);
    }
}
