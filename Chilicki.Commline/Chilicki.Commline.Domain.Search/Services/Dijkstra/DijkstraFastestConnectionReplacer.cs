using Chilicki.Commline.Domain.Search.Aggregates;
using Chilicki.Commline.Domain.Search.Aggregates.Graphs;

namespace Chilicki.Commline.Domain.Search.Services.Dijkstra
{
    public class DijkstraFastestConnectionReplacer
    {
        readonly DijkstraStopConnectionsService _dijkstraStopConnectionsService;

        public DijkstraFastestConnectionReplacer(
            DijkstraStopConnectionsService dijkstraStopConnectionsService)
        {
            _dijkstraStopConnectionsService = dijkstraStopConnectionsService;
        }

        public bool ShouldConnectionBeReplaced(
            SearchInput searchInput,
            StopConnection stopConnectionFromPreviousVertex,
            StopConnection destinationStopCurrentFastestConnection,
            StopConnection maybeNewFastestConnection)
        {
            bool isCurrentFastestConnectionEmpty = _dijkstraStopConnectionsService
                    .IsConnectionEmpty(destinationStopCurrentFastestConnection);
            bool isPreviousVertexFastestConnectionEmpty = _dijkstraStopConnectionsService
                    .IsConnectionEmpty(stopConnectionFromPreviousVertex);
            bool canMaybeNewFastestConnectionExist =
                    isPreviousVertexFastestConnectionEmpty ||
                    (searchInput.StartTime < maybeNewFastestConnection.StartTime &&
                    stopConnectionFromPreviousVertex.EndTime < maybeNewFastestConnection.EndTime);
            bool isMaybeNewFastestConnectionFaster =
                maybeNewFastestConnection.EndTime < destinationStopCurrentFastestConnection.EndTime;
            return canMaybeNewFastestConnectionExist &&
                (isCurrentFastestConnectionEmpty ||
                isMaybeNewFastestConnectionFaster);
        }

        public void ReplaceWithNewFastestConnection(
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
