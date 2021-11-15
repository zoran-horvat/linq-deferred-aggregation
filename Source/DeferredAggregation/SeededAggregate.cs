using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CodingHelmet.DeferredAggregation
{
    internal class SeededAggregate<T, TAccumulator> : IAggregatedEnumerable<T, TAccumulator>
    {
        public SeededAggregate(IEnumerable<T> sequence)
        {
            this.Sequence = sequence;
        }

        private IEnumerable<T> Sequence { get; }

        public IEnumerable<T> AsEnumerable() => this.Sequence;

        public IAggregatedEnumerable<TNew, TAccumulator> Select<TNew>(Func<T, TNew> map) =>
            new SeededAggregate<TNew, TAccumulator>(this.Sequence.Select(map));
    }
}
