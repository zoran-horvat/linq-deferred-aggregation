using System;
using System.Collections.Generic;
using CodingHelmet.DeferredAggregation.Implementation;

namespace CodingHelmet.DeferredAggregation
{
    public static class EnumerableExtensions
    {
        public static IAggregatingEnumerable<T, TAcc> AggregateStream<T, TAcc>(
            this IEnumerable<T> sequence, TAcc seed, Func<TAcc, T, TAcc> aggregator) =>
            new SequenceAggregator<T, TAcc>(sequence, seed, aggregator);
    }
}
