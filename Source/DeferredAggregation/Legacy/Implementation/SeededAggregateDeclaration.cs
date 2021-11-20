using System;
using System.Collections.Generic;

namespace CodingHelmet.DeferredAggregation.Legacy.Implementation
{
    internal class SeededAggregateDeclaration<T, TAccumulator> : IAggregatingEnumerableLegacy<T, TAccumulator>, IMaterializingAggregate<T, TAccumulator>
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
            this.MaterializeConcrete();

        private SeededAggregate<T, TAccumulator> MaterializeConcrete() =>
            new(this.Sequence, this.Seed, this.Aggregator);

        public IAggregatingEnumerableLegacy<TNew, TAccumulator> Map<TNew>(Func<IEnumerable<T>, IEnumerable<TNew>> map) =>
            this.Materialize().Map(map);

        public IAggregatingEnumerableLegacy<T, TNewAccumulator> MapAccumulator<TNewAccumulator>(Func<TAccumulator, TNewAccumulator> map) =>
            this.MaterializeConcrete().MapAccumulator(map);

        public TAccumulator Reduce() => this.Materialize().Reduce();

        public TAccumulator Reduce(Action<IEnumerable<T>> sequenceAction) => 
            this.Materialize().Reduce(sequenceAction);
    }
}
