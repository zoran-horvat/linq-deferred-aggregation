using System;
using System.Linq;

namespace CodingHelmet.DeferredAggregation
{
    public static class AggregatingLinq
    {
        public static IAggregatingEnumerable<TNew, TAcc> Select<T, TAcc, TNew>(
            this IAggregatingEnumerable<T, TAcc> sequence, Func<T, TNew> map) =>
            sequence.Specialize().Map(seq => seq.Select(map));

        public static IAggregatingEnumerable<T, TAcc> Where<T, TAcc>(
            this IAggregatingEnumerable<T, TAcc> sequence, Func<T, bool> predicate) =>
            sequence.Specialize().Map(seq => seq.Where(predicate));
    }
}
