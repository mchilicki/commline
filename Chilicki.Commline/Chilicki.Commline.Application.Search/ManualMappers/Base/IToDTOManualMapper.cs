namespace Chilicki.Commline.Application.ManualMappers.Search.Base
{
    public interface IToDTOManualMapper<TSource, TDestination>
    {
        TDestination ToDTO(TSource source);
    }
}
