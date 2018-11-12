using Chilicki.Commline.Domain.Search.Aggregates.Graphs;

namespace Chilicki.Commline.Domain.Search.Services.Dijkstra
{
    public class DijkstraIsCurrentFastestConnectionEmptyService
    {
        public bool IsEmpty(StopConnection destinationStopCurrentFastestConnection)
        {
            return destinationStopCurrentFastestConnection == null ||
                destinationStopCurrentFastestConnection.SourceStop == null;
        }
    }
}
