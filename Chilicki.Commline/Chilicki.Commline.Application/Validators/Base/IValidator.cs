namespace Chilicki.Commline.Application.Validators.Base
{
    public interface IValidator<TDTO> 
    {
        bool Validate(TDTO dto);
    }
}
