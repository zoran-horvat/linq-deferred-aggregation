using System;
using System.Collections.Generic;

namespace CodingHelmet.DeferredAggregation.Legacy.Implementation
{
    internal class SeededAggregate<T, TAccumulator> : IAggregatingImplementation<T, TAccumulator>
    {
        public SeededAggregate(
            IEnumerable<T> sequence, TAccumulator seed,
            Func<TAccumulator, T, TAccumulator> aggregator)
        {
            this.InputSequence = sequence;
            this.Accumulator = seed;
            this.Aggregator = aggregator;
            this.ProductionsCount = 0;
        }

        private IEnumerable<T> InputSequence { get; }
        public IEnumerable<T> Sequence => this.ProduceSequence();
        public TAccumulator Accumulator { get; set; }
        private Func<TAccumulator, T, TAccumulator> Aggregator { get; }
        private int ProductionsCount { get; set; }

        public IEnumerable<T> AsEnumerable() => this.InputSequence;

        private IEnumerable<T> ProduceSequence()
        {
            if (this.ProductionsCount > 0)
            {
                throw new InvalidOperationException("Aggregating sequence cannot be iterated more than once.");
            }

            this.ProductionsCount += 1;
            foreach (T item in this.InputSequence)
            {
                this.Accumulator = this.Aggregator(this.Accumulator, item);
                yield return item;
            }
        }

        public IAggregatingEnumerableLegacy<TNew, TAccumulator> Map<TNew>(Func<IEnumerable<T>, IEnumerable<TNew>> map) =>
            new AggregateHandle<T, TAccumulator>(this.ProduceSequence(), () => this.Accumulator).Map(map);

        public IAggregatingEnumerableLegacy<T, TNewAccumulator> MapAccumulator<TNewAccumulator>(Func<TAccumulator, TNewAccumulator> map) =>
            new AggregateHandle<T, TNewAccumulator>(this.ProduceSequence(), () => map(this.Accumulator));

        public TAccumulator Reduce()
        {
            foreach (T _ in this.ProduceSequence()) { }
            return this.Accumulator;
        }

        public TAccumulator Reduce(Action<IEnumerable<T>> sequenceAction)
        {
            sequenceAction(this.ProduceSequence());
            return this.Accumulator;
        }
    }
}
