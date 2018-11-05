using Chilicki.Commline.Domain.Entities;
using Chilicki.Commline.Domain.Search.Aggregates.Graph;
using Chilicki.Commline.Domain.Search.Services.GraphGenerator.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chilicki.Commline.Domain.Search.Services.GraphGenerator
{
    public class StopGraphGenerator : IGraphGenerator<StopGraph>
    {
        public StopGraph GenerateGraph(IEnumerable<Line> lines)
        {
            var toList = lines.ToList();
            throw new NotImplementedException();
        }
    }
}
