namespace CodingHelmet.DeferredAggregation.ImplementationLegacy
{
    internal interface IMaterializingAggregate<T, TAccumulator>
    {
        IAggregatingImplementation<T, TAccumulator> Materialize();
    }
}
