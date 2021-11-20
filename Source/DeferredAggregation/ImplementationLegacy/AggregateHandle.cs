using System;
using System.Collections.Generic;

namespace CodingHelmet.DeferredAggregation.ImplementationLegacy
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

        public IAggregatingEnumerableLegacy<TNew, TAccumulator> Map<TNew>(Func<IEnumerable<T>, IEnumerable<TNew>> map) =>
            new AggregateHandle<TNew, TAccumulator>(map(this.Sequence), this.AccumulatorHandle);

        public IAggregatingEnumerableLegacy<T, TNewAccumulator> MapAccumulator<TNewAccumulator>(Func<TAccumulator, TNewAccumulator> map) =>
            new AggregateHandle<T, TNewAccumulator>(this.Sequence, () => map(this.AccumulatorHandle()));

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
