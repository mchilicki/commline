using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chilicki.Commline.Application.ManualMappers.Base
{
    public interface IToDomainManualMapper<TSource, TDestination>
    {
        TDestination ToDomain(TSource source);
    }
}
