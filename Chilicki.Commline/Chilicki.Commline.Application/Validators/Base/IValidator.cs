using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chilicki.Commline.Application.Validators.Base
{
    public interface IValidator<TDTO> 
    {
        bool Validate(TDTO dto);
    }
}
