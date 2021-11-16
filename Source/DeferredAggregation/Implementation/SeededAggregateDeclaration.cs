using System;
using System.Collections.Generic;

namespace CodingHelmet.DeferredAggregation.Implementation
{
    internal class SeededAggregateDeclaration<T, TAccumulator> : IAggregatingEnumerable<T, TAccumulator>, IMaterializingAggregate<T, TAccumulator>
    {
        public SeededAggregateDeclaration(
            IEnumerable<T> sequence, TAccumulator seed,
            Func<TAccumulator, T, TAccumulator> aggregator)
        {
            this.Sequence = sequence;
            this.Seed = seed;
            this.Aggregator = aggregator;
        }

        private IEnumerable<T> Sequence { get; }
        private TAccumulator Seed { get; }
        private Func<TAccumulator, T, TAccumulator> Aggregator { get; }

        public IEnumerable<T> AsEnumerable() => this.Sequence;

        public IAggregatingImplementation<T, TAccumulator> Materialize() =>
            new SeededAggregate<T, TAccumulator>(this.Sequence, this.Seed, this.Aggregator);

        public IAggregatingEnumerable<TNew, TAccumulator> MapData<TNew>(Func<IEnumerable<T>, IEnumerable<TNew>> map) =>
            this.Materialize().MapData(map);

        public TAccumulator Reduce() => this.Materialize().Reduce();

        public TAccumulator Reduce(Action<IEnumerable<T>> sequenceAction) => 
            this.Materialize().Reduce(sequenceAction);
    }
}
