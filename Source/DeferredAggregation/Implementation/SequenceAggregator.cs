using System;
using System.Collections.Generic;

namespace CodingHelmet.DeferredAggregation.Implementation
{
    internal class SequenceAggregator<T, TAcc> : IAggregatingEnumerable<T, TAcc>
    {
        public SequenceAggregator(ElementAggregator<T, TAcc> aggregator)
            : this(aggregator.Sequence, aggregator.GetAccumulator)
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
