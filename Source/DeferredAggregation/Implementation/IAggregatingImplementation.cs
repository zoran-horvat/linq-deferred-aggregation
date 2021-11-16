using System.Collections.Generic;

namespace CodingHelmet.DeferredAggregation.Implementation
{
    internal interface IAggregatingImplementation<T, TAccumulator> : IAggregatingEnumerable<T, TAccumulator>
    {
        TAccumulator Accumulator { get; }
        IEnumerable<T> Sequence { get; }
    }
}
