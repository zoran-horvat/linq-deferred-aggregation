using System;
using System.Collections.Generic;

namespace CodingHelmet.DeferredAggregation
{
    public interface IAggregatingEnumerable<T, TAcc>
    {
        TAcc Reduce() =>
            throw new NotImplementedException();

        TAcc Reduce(Action<IEnumerable<T>> sequenceAction) =>
            throw new NotImplementedException();
    }
}
