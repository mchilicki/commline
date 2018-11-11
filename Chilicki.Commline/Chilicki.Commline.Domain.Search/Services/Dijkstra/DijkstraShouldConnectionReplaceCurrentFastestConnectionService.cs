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
            StopConnection destinationStopCurrentFastestConnection,
            StopConnection currentStopConnection)
        {
            return (_dijkstraIsCurrentFastestConnectionEmptyService
                    .IsEmpty(destinationStopCurrentFastestConnection) ||
                (searchInput.StartTime < destinationStopCurrentFastestConnection.StartTime &&
                currentStopConnection.EndTime < destinationStopCurrentFastestConnection.EndTime));
        }
    }
}