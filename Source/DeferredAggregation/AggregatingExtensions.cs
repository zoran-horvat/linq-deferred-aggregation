using System;
using CodingHelmet.DeferredAggregation.Implementation;

namespace CodingHelmet.DeferredAggregation
{
    public static class AggregatingExtensions
    {
        public static IMultiAggregatingEnumerable<T, (TAcc1, TAcc2)> AggregateStream<T, TAcc1, TAcc2>(
            this IAggregatingEnumerable<T, TAcc1> sequence,
            TAcc2 seed, Func<TAcc2, T, TAcc2> aggregator) =>
            throw new NotImplementedException();

        internal static SequenceAggregator<T, TAcc> Specialize<T, TAcc>(
            this IAggregatingEnumerable<T, TAcc> sequence) =>
            sequence is SequenceAggregator<T, TAcc> aggregator ? aggregator
            : throw new InvalidOperationException($"Could not process {sequence?.GetType().Name ?? "<null>"}");
    }
}
