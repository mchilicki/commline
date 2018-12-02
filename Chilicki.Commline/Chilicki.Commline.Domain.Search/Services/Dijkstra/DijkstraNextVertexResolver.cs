using Chilicki.Commline.Domain.Entities;
using Chilicki.Commline.Domain.Search.Aggregates.Graphs;
using System.Collections.Generic;

namespace Chilicki.Commline.Domain.Search.Services.Dijkstra
{
    public class DijkstraNextVertexResolver
    {
        readonly DijkstraStopConnectionsService _dijkstraStopConnectionsService;
        readonly DijkstraStopGraphService _dijkstraStopGraphService; 

        public DijkstraNextVertexResolver(
            DijkstraStopConnectionsService dijkstraStopConnectionsService,
            DijkstraStopGraphService dijkstraStopGraphService)
        {
            _dijkstraStopConnectionsService = dijkstraStopConnectionsService;
            _dijkstraStopGraphService = dijkstraStopGraphService;
        }

        public StopVertex GetFirstVertex(StopGraph graph, Stop startingStop)
        {
            return _dijkstraStopGraphService
                .GetStopVertexByStop(graph, startingStop);         
        }

        public StopVertex GetNextVertex(StopGraph graph, 
            IEnumerable<StopConnection> vertexFastestConnections)
        {
            StopConnection fastestConnection = null;
            foreach (var maybeNewFastestConnection in vertexFastestConnections)
            {
                if (!_dijkstraStopConnectionsService.IsConnectionEmpty(maybeNewFastestConnection))
                {
                    if (!maybeNewFastestConnection.DestinationStop.IsVisited)
                    {
                        if (fastestConnection == null || 
                            fastestConnection.EndDateTime > maybeNewFastestConnection.EndDateTime)
                        {
                            fastestConnection = maybeNewFastestConnection;
                        }                            
                    }
                }
            }
            if (fastestConnection == null)
                return null;
            return fastestConnection.DestinationStop;
        }
    }
}
