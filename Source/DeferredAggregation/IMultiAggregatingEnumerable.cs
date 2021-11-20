using System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;

namespace CodingHelmet.DeferredAggregation
{
    public interface IMultiAggregatingEnumerable<T, TAcc> where TAcc : ITuple
    {
        public TAcc Reduce() =>
            throw new NotImplementedException();

        public TAcc Reduce(Action<IEnumerable<T>> sequenceAction) =>
            throw new NotImplementedException();
    }
}
