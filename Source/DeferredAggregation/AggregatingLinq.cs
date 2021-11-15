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
    }
}
