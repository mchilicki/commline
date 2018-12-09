namespace Chilicki.Commline.Application.Correctors.Base
{
    public interface ICorrector<DTO>
    {
        DTO Correct(DTO dto);
    }
}
