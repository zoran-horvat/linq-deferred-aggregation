using System.Collections.Generic;

namespace CodingHelmet.DeferredAggregation.Implementation
{
    internal abstract class ElementAggregator<T, TAcc>
    {
        public abstract IEnumerable<T> Sequence { get; }
        public abstract TAcc GetAccumulator();
    }
}
