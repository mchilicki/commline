using Chilicki.Commline.Domain.Entities;
using Chilicki.Commline.Domain.Search.Aggregates.Graphs;
using System.Linq;

namespace Chilicki.Commline.Domain.Search.Services.Dijkstra
{
    public class DijkstraStopGraphService
    {
        public StopVertex GetStopVertexByStop(StopGraph graph, Stop stop)
        {
            return graph
                .StopVertices
                .First(p => p.Stop.Id == stop.Id);
        }

        public StopVertex MarkVertexAsVisited(StopVertex stopVertex)
        {
            stopVertex.IsVisited = true;
            return stopVertex;
        }
    }
}
