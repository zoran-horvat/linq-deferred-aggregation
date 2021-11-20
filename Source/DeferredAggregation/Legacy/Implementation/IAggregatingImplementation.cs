using System.Collections.Generic;

namespace CodingHelmet.DeferredAggregation.Legacy.Implementation
{
    internal interface IAggregatingImplementation<T, TAccumulator> : IAggregatingEnumerableLegacy<T, TAccumulator>
    {
        TAccumulator Accumulator { get; }
        IEnumerable<T> Sequence { get; }
    }
}
