using System;
using System.Collections.Generic;

namespace CodingHelmet.DeferredAggregation.Implementation
{
    internal class SeededAggregator<T, TAcc>
    {
        public SeededAggregator(
            IEnumerable<T> sequence, TAcc seed,
            Func<TAcc, T, TAcc> aggregator)
        {
            this.Sequence = sequence;
        }

        public IEnumerable<T> Sequence { get; }
        public TAcc Accumulator { get; }
    }
}
