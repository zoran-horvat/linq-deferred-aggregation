using System;
using System.Collections.Generic;

namespace CodingHelmet.DeferredAggregation
{
    public interface IAggregatedEnumerable<T, TAccumulator>
    {
        IEnumerable<T> AsEnumerable();
        IAggregatedEnumerable<TNew, TAccumulator> Select<TNew>(Func<T, TNew> map);
    }
}
