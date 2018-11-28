using System.Collections.Generic;

namespace Chilicki.Commline.Application.Validators.Base
{
    public interface IRemoveValidator<TDTO>
    {
        bool ValidateRemove(TDTO dto);
        bool ValidateRemove(IEnumerable<TDTO> dtoList);
    }
}
