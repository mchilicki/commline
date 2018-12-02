using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chilicki.Commline.Application.Correctors.Base
{
    public interface ICorrector<DTO>
    {
        DTO Correct(DTO dto);
    }
}
