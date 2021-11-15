using System;
using System.Collections.Generic;
using CodingHelmet.DeferredAggregation.Implementation;

namespace CodingHelmet.DeferredAggregation
{
    public static class EnumerableExtensions
    {
        public static IAggregatingEnumerable<T, TAccumulator> AggregateStream<T, TAccumulator>(
            this IEnumerable<T> sequence, TAccumulator seed,
            Func<TAccumulator, T, TAccumulator> aggregator) =>
            new SeededAggregateDeclaration<T, TAccumulator>(sequence, seed, aggregator);
    }
}
