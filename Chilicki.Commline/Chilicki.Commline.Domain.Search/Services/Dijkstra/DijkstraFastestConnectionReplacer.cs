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
            bool isDestinationVertexMarkedAsVisited = destinationStopCurrentFastestConnection
                .DestinationStop.IsVisited;
            bool isCurrentFastestConnectionEmpty = _dijkstraStopConnectionsService
                    .IsConnectionEmpty(destinationStopCurrentFastestConnection);
            bool isPreviousVertexFastestConnectionEmpty = _dijkstraStopConnectionsService
                    .IsConnectionEmpty(stopConnectionFromPreviousVertex);
            bool canMaybeNewFastestConnectionExist =
                searchInput.StartFullDate <= maybeNewFastestConnection.StartDateTime &&
                    (isPreviousVertexFastestConnectionEmpty ||
                    stopConnectionFromPreviousVertex.EndDateTime <= maybeNewFastestConnection.StartDateTime);
            bool isMaybeNewFastestConnectionFaster =
                maybeNewFastestConnection.EndDateTime < destinationStopCurrentFastestConnection.EndDateTime;
            if (isDestinationVertexMarkedAsVisited)
                return false;
            return canMaybeNewFastestConnectionExist &&
                (isCurrentFastestConnectionEmpty ||
                isMaybeNewFastestConnectionFaster);
        }

        public void ReplaceWithNewFastestConnection(
            StopConnection currentFastestStopConnection, 
            StopConnection newFastestConnection)
        {
            currentFastestStopConnection.Line = newFastestConnection.Line;
            currentFastestStopConnection.StartDateTime = newFastestConnection.StartDateTime;
            currentFastestStopConnection.SourceStop = newFastestConnection.SourceStop;
            currentFastestStopConnection.EndDateTime = newFastestConnection.EndDateTime;
        }
    }
}
