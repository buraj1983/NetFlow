using System;
namespace NetFlow.Infrastructure.Converters
{
    public interface IConverter<TSource, TDestination>
    {
        TDestination To(TSource source);
    }
}
