using Chilicki.Commline.Domain.Entities;
using System.Collections.Generic;

namespace Chilicki.Commline.Domain.Search.Services.GraphGenerator.Base
{
    public interface IGraphGenerator<TGraph>
    {
        TGraph GenerateGraph(IEnumerable<Line> lines);
    }
}