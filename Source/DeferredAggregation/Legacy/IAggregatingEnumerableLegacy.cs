using System;
using System.Collections.Generic;

namespace CodingHelmet.DeferredAggregation.Legacy
{
    public interface IAggregatingEnumerableLegacy<T, TAccumulator>
    {
        IAggregatingEnumerableLegacy<TNew, TAccumulator> Map<TNew>(Func<IEnumerable<T>, IEnumerable<TNew>> map);
        TAccumulator Reduce();
        TAccumulator Reduce(Action<IEnumerable<T>> sequenceAction);
    }
}
