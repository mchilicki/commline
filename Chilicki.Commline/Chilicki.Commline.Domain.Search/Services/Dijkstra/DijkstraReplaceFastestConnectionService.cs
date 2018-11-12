using Chilicki.Commline.Domain.Search.Aggregates.Graphs;

namespace Chilicki.Commline.Domain.Search.Services.Dijkstra
{
    public class DijkstraReplaceFastestConnectionService
    {
        public void Replace(
            StopConnection currentFastestStopConnection, 
            StopConnection newFastestConnection)
        {
            currentFastestStopConnection.Line = newFastestConnection.Line;
            currentFastestStopConnection.StartTime = newFastestConnection.StartTime;
            currentFastestStopConnection.SourceStop = newFastestConnection.SourceStop;
            currentFastestStopConnection.EndTime = newFastestConnection.EndTime;
        }
    }
}
