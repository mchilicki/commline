namespace Chilicki.Commline.Application.ManualMappers.Search.Base
{
    public interface IToDomainManualMapper<TSource, TDestination>
    {
        TDestination ToDomain(TSource source);
    }
}
