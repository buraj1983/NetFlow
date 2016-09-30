using System;
using System.Collections.Concurrent;

namespace NetFlow.CrossCutting.Caching
{
    public static class Memoization
    {
        public static Func<TInput, TOutput> Memoize<TInput, TOutput>(this Func<TInput, TOutput> func)
        {
            var lookup = new ConcurrentDictionary<TInput,TOutput>();
            return arg => lookup.GetOrAdd(arg, func);
        }
    }
}
