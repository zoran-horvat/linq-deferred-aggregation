using System;
using System.Collections.Generic;

namespace CodingHelmet.DeferredAggregation.Implementation
{
    internal class SeededAggregator<T, TAcc> : ElementAggregator<T, TAcc>
    {
        public SeededAggregator(
            IEnumerable<T> sequence, TAcc seed,
            Func<TAcc, T, TAcc> aggregator)
        {
            this.InputSequence = sequence;
            this.Accumulator = seed;
            this.AggregationFunction = aggregator;
        }

        private IEnumerable<T> InputSequence { get; }
        private TAcc Accumulator { get; set; }
        Func<TAcc, T, TAcc> AggregationFunction { get; }

        public override IEnumerable<T> Sequence
        {
            get
            {
                foreach (T item in this.InputSequence)
                {
                    this.Accumulator = this.AggregationFunction(this.Accumulator, item);
                    yield return item;
                }
            }
        }

        public override TAcc GetAccumulator() => this.Accumulator;

    }
}
