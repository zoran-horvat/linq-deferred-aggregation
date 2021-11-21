using System;
using System.Collections.Generic;

namespace CodingHelmet.DeferredAggregation.Implementation
{
    internal class SequenceAggregator<T, TAcc> : IAggregatingEnumerable<T, TAcc>
    {
        public SequenceAggregator(
            IEnumerable<T> sequence,
            TAcc seed, Func<TAcc, T, TAcc> aggregator)
            : this(new SeededAggregator<T, TAcc>(sequence, seed, aggregator))
        {
        }

        private SequenceAggregator(SeededAggregator<T, TAcc> aggregator)
            : this(aggregator.Sequence, () => aggregator.Accumulator)
        {
        }

        private SequenceAggregator(IEnumerable<T> sequence, Func<TAcc> accumulatorHandle)
        {
            this.Sequence = sequence;
            this.AccumulatorHandle = accumulatorHandle;
        }

        private IEnumerable<T> Sequence { get; }
        private Func<TAcc> AccumulatorHandle { get; }

        public TAcc Reduce() => this.AccumulatorHandle();

        public TAcc Reduce(Action<IEnumerable<T>> sequenceAction)
        {
            sequenceAction(this.Sequence);
            return this.AccumulatorHandle();
        }

        internal IAggregatingEnumerable<TNew, TAcc> Map<TNew>(Func<IEnumerable<T>, IEnumerable<TNew>> map) =>
            new SequenceAggregator<TNew, TAcc>(map(this.Sequence), this.AccumulatorHandle);
    }
}
