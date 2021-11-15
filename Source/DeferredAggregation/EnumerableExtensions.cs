using System;
using System.Collections.Generic;

namespace CodingHelmet.DeferredAggregation
{

    public static class EnumerableExtensions
    {
        public static IAggregatedEnumerable<T, TAccumulator> AggregateStream<T, TAccumulator>(
            this IEnumerable<T> sequence, TAccumulator seed,
            Func<TAccumulator, T, TAccumulator> aggregator) =>
            new SeededAggregate<T, TAccumulator>(sequence);
    }
}
