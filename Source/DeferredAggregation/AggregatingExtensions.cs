using System;

namespace CodingHelmet.DeferredAggregation
{
    public static class AggregatingExtensions
    {
        public static IMultiAggregatingEnumerable<T, (TAcc1, TAcc2)> AggregateStream<T, TAcc1, TAcc2>(
            this IAggregatingEnumerable<T, TAcc1> sequence,
            TAcc2 seed, Func<TAcc2, T, TAcc2> aggregator) =>
            throw new NotImplementedException();
    }
}
