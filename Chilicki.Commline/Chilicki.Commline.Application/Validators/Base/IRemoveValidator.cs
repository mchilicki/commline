using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chilicki.Commline.Application.Validators.Base
{
    public interface IRemoveValidator<TDTO>
    {
        bool ValidateRemove(TDTO dto);
        bool ValidateRemove(IEnumerable<TDTO> dtoList);
    }
}
