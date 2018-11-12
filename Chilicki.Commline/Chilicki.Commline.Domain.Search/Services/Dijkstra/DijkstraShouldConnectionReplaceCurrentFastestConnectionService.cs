using Chilicki.Commline.Domain.Search.Aggregates;
using Chilicki.Commline.Domain.Search.Aggregates.Graphs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chilicki.Commline.Domain.Search.Services.Dijkstra
{
    public class DijkstraShouldConnectionReplaceCurrentFastestConnectionService
    {
        readonly DijkstraIsCurrentFastestConnectionEmptyService _dijkstraIsCurrentFastestConnectionEmptyService;

        public DijkstraShouldConnectionReplaceCurrentFastestConnectionService(
            DijkstraIsCurrentFastestConnectionEmptyService dijkstraIsCurrentFastestConnectionEmptyService)
        {
            _dijkstraIsCurrentFastestConnectionEmptyService = dijkstraIsCurrentFastestConnectionEmptyService;
        }

        public bool Return(
            SearchInput searchInput,
            StopConnection stopConnectionFromPreviousVertex,
            StopConnection destinationStopCurrentFastestConnection,
            StopConnection maybeNewFastestConnection)
        {
            bool isCurrentFastestConnectionEmpty = _dijkstraIsCurrentFastestConnectionEmptyService
                    .IsEmpty(destinationStopCurrentFastestConnection);
            bool isPreviousVertexFastestConnectionEmpty = _dijkstraIsCurrentFastestConnectionEmptyService
                    .IsEmpty(stopConnectionFromPreviousVertex);
            bool canMaybeNewFastestConnectionExist =
                    isPreviousVertexFastestConnectionEmpty ||
                    (searchInput.StartTime < maybeNewFastestConnection.StartTime &&
                    stopConnectionFromPreviousVertex.EndTime < maybeNewFastestConnection.EndTime);
            //if (isCurrentFastestConnectionEmpty)
            //    canMaybeNewFastestConnectionExist =
            //        isPreviousVertexFastestConnectionEmpty ||
            //        stopConnectionFromPreviousVertex.EndTime < maybeNewFastestConnection.EndTime;
            //else
            bool isMaybeNewFastestConnectionFaster = 
                maybeNewFastestConnection.EndTime < destinationStopCurrentFastestConnection.EndTime;
            return canMaybeNewFastestConnectionExist &&
                (isCurrentFastestConnectionEmpty ||
                isMaybeNewFastestConnectionFaster);
        }
    }
}