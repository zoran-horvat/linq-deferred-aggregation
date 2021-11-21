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
            this.Sequence = sequence;
            this.Accumulator = seed;
        }

        public override IEnumerable<T> Sequence { get; }
        public override TAcc GetAccumulator() => this.Accumulator;

        private TAcc Accumulator { get; }
    }
}
