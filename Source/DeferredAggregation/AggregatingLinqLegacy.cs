using System;
using System.Linq;

namespace CodingHelmet.DeferredAggregation
{
    public static class AggregatingLinqLegacy
    {
        public static IAggregatingEnumerableLegacy<TNew, TAccumulator> Select<T, TAccumulator, TNew>(
            this IAggregatingEnumerableLegacy<T, TAccumulator> sequence,
            Func<T, TNew> map) =>
            sequence.Map(seq => seq.Select(map));

        public static IAggregatingEnumerableLegacy<T, TAccumulator> Where<T, TAccumulator>(
            this IAggregatingEnumerableLegacy<T, TAccumulator> sequence,
            Func<T, bool> predicate) =>
            sequence.Map(seq => seq.Where(predicate));
    }
}
