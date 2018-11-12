using Chilicki.Commline.Common.Extensions;
using Chilicki.Commline.Domain.Search.Aggregates.Graphs;
using System.Collections.Generic;
using System.Linq;

namespace Chilicki.Commline.Domain.Search.Services.Dijkstra
{
    public class DijkstraStopConnectionsService
    {
        public StopConnection GetDestinationStopFastestConnection(
            IEnumerable<StopConnection> vertexFastestConnections,
            StopConnection stopConnection)
        {
            return vertexFastestConnections
                        .First(p => p.DestinationStop.Stop.Id == 
                            stopConnection.DestinationStop.Stop.Id);
        }

        public StopConnection GetStopConnectionFromPreviousVertex(
            IEnumerable<StopConnection> vertexFastestConnections,
            StopConnection stopConnection)
        {
            if (stopConnection.SourceStop == null)
                return null;
            return vertexFastestConnections
                    .FirstOrNull(p => p.SourceStop != null &&
                        p.SourceStop.Stop.Id == stopConnection.SourceStop.Stop.Id);
        }

        public bool IsConnectionEmpty(StopConnection stopConnection)
        {
            return stopConnection == null ||
                stopConnection.SourceStop == null;
        }
    }
}
