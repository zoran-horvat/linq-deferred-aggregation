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

        public static IAggregatingEnumerable<T, (TAccumulator1, TAccumulator2)> AggregateStream<T, TAccumulator1, TAccumulator2>(
            this IAggregatingEnumerable<T, TAccumulator1> sequence,
            TAccumulator2 seed, Func<TAccumulator2, T, TAccumulator2> aggregator) =>
            sequence.AsImplementation().AggregateStream(seed, aggregator);

        public static IAggregatingEnumerable<T, TAccumulator> AggregateStream<T, TAccumulator1, TAccumulator2, TAccumulator>(
            this IAggregatingEnumerable<T, TAccumulator1> sequence,
            TAccumulator2 seed, Func<TAccumulator2, T, TAccumulator2> aggregator,
            Func<TAccumulator1, TAccumulator2, TAccumulator> combiner) =>
            sequence.AsImplementation()
                .AggregateStream(seed, aggregator)
                .MapAccumulator(((TAccumulator1 a, TAccumulator2 b) tuple) => combiner(tuple.a, tuple.b));

        private static SeededAggregateDeclaration<T, (TAccumulator1 acc1, TAccumulator2 acc2)> AggregateStream<T, TAccumulator1, TAccumulator2>(
            this IAggregatingImplementation<T, TAccumulator1> sequence,
            TAccumulator2 seed, Func<TAccumulator2, T, TAccumulator2> aggregator) =>
            new(sequence.Sequence, (acc1: sequence.Accumulator, acc2: seed),
                (acc, element) => (sequence.Accumulator, aggregator(acc.acc2, element)));

        private static IAggregatingImplementation<T, TAccumulator> AsImplementation<T, TAccumulator>(
            this IAggregatingEnumerable<T, TAccumulator> sequence) =>
            sequence is IAggregatingImplementation<T, TAccumulator> implementation ? implementation
            : sequence is IMaterializingAggregate<T, TAccumulator> materializing ? materializing.Materialize()
            : throw new InvalidOperationException($"Chained aggregation not applicable to object of type {sequence?.GetType().Name ?? "<null>"}");
    }
}
