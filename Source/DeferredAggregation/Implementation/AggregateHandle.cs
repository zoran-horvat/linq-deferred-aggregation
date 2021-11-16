using System;
using System.Collections.Generic;

namespace CodingHelmet.DeferredAggregation.Implementation
{
    internal class AggregateHandle<T, TAccumulator> : IAggregatingImplementation<T, TAccumulator>
    {
        public AggregateHandle(IEnumerable<T> sequence, Func<TAccumulator> accumulatorHandle)
        {
            this.Sequence = sequence;
            this.AccumulatorHandle = accumulatorHandle;
        }

        public IEnumerable<T> Sequence { get; }
        private Func<TAccumulator> AccumulatorHandle { get; }
        public TAccumulator Accumulator => this.AccumulatorHandle();

        public IEnumerable<T> AsEnumerable() => this.Sequence;

        public IAggregatingEnumerable<TNew, TAccumulator> MapData<TNew>(Func<IEnumerable<T>, IEnumerable<TNew>> map) =>
            new AggregateHandle<TNew, TAccumulator>(map(this.Sequence), this.AccumulatorHandle);

        public TAccumulator Reduce()
        {
            foreach (T _ in this.Sequence) { }
            return this.AccumulatorHandle();
        }

        public TAccumulator Reduce(Action<IEnumerable<T>> sequenceAction)
        {
            sequenceAction(this.Sequence);
            return this.AccumulatorHandle();
        }
    }
}
