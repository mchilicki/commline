using Chilicki.Commline.Domain.Entities;
using System.Collections.Generic;

namespace Chilicki.Commline.Domain.Search.Aggregates.Graph
{
    public class StopVertex
    {
        public Stop Stop { get; set; }
        public IEnumerable<StopConnection> StopConnections { get; set; }
    }
}
