using Chilicki.Commline.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Chilicki.Commline.Domain.Search.Services.GraphFactories.Base
{
    public interface IGraphFactory<TGraph>
    {
        TGraph CreateGraph(IEnumerable<Stop> stops, DateTime day);
    }
}