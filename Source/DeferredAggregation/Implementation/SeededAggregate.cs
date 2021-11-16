using System;
using System.Collections.Generic;

namespace CodingHelmet.DeferredAggregation.Implementation
{
    internal class SeededAggregate<T, TAccumulator> : IAggregatingEnumerable<T, TAccumulator>
    {
        public SeededAggregate(
            IEnumerable<T> sequence, TAccumulator seed,
            Func<TAccumulator, T, TAccumulator> aggregator)
        {
            this.Sequence = sequence;
            this.Accumulator = seed;
            this.Aggregator = aggregator;
            this.ProductionsCount = 0;
        }

        private IEnumerable<T> Sequence { get; }
        private TAccumulator Accumulator { get; set; }
        private Func<TAccumulator, T, TAccumulator> Aggregator { get; }
        private int ProductionsCount { get; set; }

        public IEnumerable<T> AsEnumerable() => this.Sequence;

        private IEnumerable<T> ProduceSequence()
        {
            if (this.ProductionsCount > 0)
            {
                throw new InvalidOperationException("Aggregating sequence cannot be iterated more than once.");
            }

            this.ProductionsCount += 1;
            foreach (T item in this.Sequence)
            {
                this.Accumulator = this.Aggregator(this.Accumulator, item);
                yield return item;
            }
        }

        public IAggregatingEnumerable<TNew, TAccumulator> MapData<TNew>(Func<IEnumerable<T>, IEnumerable<TNew>> map) =>
            new AggregateHandle<T, TAccumulator>(this.ProduceSequence(), () => this.Accumulator).MapData(map);

        public TAccumulator Reduce()
        {
            foreach (T _ in this.ProduceSequence()) { }
            return this.Accumulator;
        }

        public TAccumulator Reduce(Action<IEnumerable<T>> sequenceAction)
        {
            sequenceAction(this.Sequence);
            return this.Accumulator;
        }
    }
}
