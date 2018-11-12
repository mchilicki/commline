using Chilicki.Commline.Domain.Entities;
using Chilicki.Commline.Domain.Search.Aggregates.Graphs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chilicki.Commline.Domain.Search.Services.Dijkstra
{
    public class DijkstraNextVertexResolver
    {
        readonly DijkstraIsCurrentFastestConnectionEmptyService _dijkstraIsCurrentFastestConnectionEmptyService;
        readonly DijkstraStopGraphService _dijkstraStopGraphService; 

        public DijkstraNextVertexResolver(
            DijkstraIsCurrentFastestConnectionEmptyService dijkstraIsCurrentFastestConnectionEmptyService,
            DijkstraStopGraphService dijkstraStopGraphService)
        {
            _dijkstraIsCurrentFastestConnectionEmptyService = dijkstraIsCurrentFastestConnectionEmptyService;
            _dijkstraStopGraphService = dijkstraStopGraphService;
        }

        public StopVertex GetFirstVertex(StopGraph graph, Stop startingStop)
        {
            return graph
                .StopVertices
                .First(p => p.Stop.Id == startingStop.Id);         
        }

        public StopVertex GetNextVertex(StopGraph graph, 
            IEnumerable<StopConnection> vertexFastestConnections)
        {
            StopConnection fastestConnection = null;
            foreach (var maybeNewFastestConnection in vertexFastestConnections)
            {
                if (!_dijkstraIsCurrentFastestConnectionEmptyService.IsEmpty(maybeNewFastestConnection))
                {
                    if (!maybeNewFastestConnection.DestinationStop.IsVisited)
                    {
                        if (fastestConnection == null || 
                            fastestConnection.EndTime > maybeNewFastestConnection.EndTime)
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
