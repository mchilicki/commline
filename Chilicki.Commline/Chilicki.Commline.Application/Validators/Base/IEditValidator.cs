using System.Collections.Generic;

namespace Chilicki.Commline.Application.Validators.Base
{
    public interface IEditValidator<TDTO>
    {
        bool ValidateEdit(TDTO dto);
        bool ValidateEdit(IEnumerable<TDTO> dtoList);
    }
}
