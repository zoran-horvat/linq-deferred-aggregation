using System;
using System.Collections.Generic;

namespace CodingHelmet.DeferredAggregation
{
    public static class EnumerableExtensions
    {
        public static IAggregatingEnumerable<T, TAcc> AggregateStream<T, TAcc>(
            this IEnumerable<T> sequence, TAcc seed, Func<TAcc, T, TAcc> aggregator) =>
            throw new NotImplementedException();
    }
}
