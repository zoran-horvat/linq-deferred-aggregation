namespace CodingHelmet.DeferredAggregation.Legacy.Implementation
{
    internal interface IMaterializingAggregate<T, TAccumulator>
    {
        IAggregatingImplementation<T, TAccumulator> Materialize();
    }
}
