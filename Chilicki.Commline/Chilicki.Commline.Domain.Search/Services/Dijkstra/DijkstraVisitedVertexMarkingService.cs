using Chilicki.Commline.Domain.Search.Aggregates.Graphs;

namespace Chilicki.Commline.Domain.Search.Services.Dijkstra
{
    public class DijkstraVisitedVertexMarkingService
    {
        public StopVertex MarkAsVisited(StopVertex stopVertex)
        {
            stopVertex.IsVisited = true;
            return stopVertex;
        } 
    }
}
