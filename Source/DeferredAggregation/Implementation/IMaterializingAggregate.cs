namespace CodingHelmet.DeferredAggregation.Implementation
{
    internal interface IMaterializingAggregate<T, TAccumulator>
    {
        IAggregatingImplementation<T, TAccumulator> Materialize();
    }
}
