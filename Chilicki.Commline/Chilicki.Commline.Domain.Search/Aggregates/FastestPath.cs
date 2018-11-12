using Chilicki.Commline.Domain.Search.Aggregates.Graphs;
using System.Collections.Generic;

namespace Chilicki.Commline.Domain.Search.Aggregates
{
    public class FastestPath
    {
        public IEnumerable<StopConnection> Path { get; set; }
    }
}
