using System;
using System.Linq;

namespace CodingHelmet.DeferredAggregation
{
    public static class AggregatingLinq
    {
        public static IAggregatingEnumerable<TNew, TAccumulator> Select<T, TAccumulator, TNew>(
            this IAggregatingEnumerable<T, TAccumulator> sequence,
            Func<T, TNew> map) =>
            sequence.MapData(seq => seq.Select(map));

        public static IAggregatingEnumerable<T, TAccumulator> Where<T, TAccumulator>(
            this IAggregatingEnumerable<T, TAccumulator> sequence,
            Func<T, bool> predicate) =>
            sequence.MapData(seq => seq.Where(predicate));
    }
}
