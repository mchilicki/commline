using System.Collections.Generic;

namespace Chilicki.Commline.Application.Validators.Base
{
    public interface IValidator<TDTO> 
    {
        bool Validate(TDTO dto);
        bool Validate(IEnumerable<TDTO> dtoList);
    }
}
