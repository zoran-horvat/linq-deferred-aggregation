using System;

namespace CodingHelmet.DeferredAggregation
{
    public static class AggregatingLinq
    {
        public static IAggregatingEnumerable<TNew, TAcc> Select<T, TAcc, TNew>(
            this IAggregatingEnumerable<T, TAcc> sequence, Func<T, TNew> map) =>
            throw new NotImplementedException();

        public static IAggregatingEnumerable<T, TAcc> Where<T, TAcc>(
            this IAggregatingEnumerable<T, TAcc> sequence, Func<T, bool> predicate) =>
            throw new NotImplementedException();
    }
}
