using Chilicki.Commline.Domain.Search.Aggregates;
using Chilicki.Commline.Domain.Search.Aggregates.Graphs;
using Chilicki.Commline.Domain.Search.Factories.Dijkstra;
using Chilicki.Commline.Domain.Search.Services.Base;
using Chilicki.Commline.Domain.Search.Services.Dijkstra;
using System.Collections.Generic;

namespace Chilicki.Commline.Domain.Search.Services
{
    public class DijkstraConnectionSearchEngine : IConnectionSearchEngine
    {
        readonly DijkstraEmptyFastestConnectionsArrayFactory _dijkstraEmptyFastestConnectionsArrayFactory;
        readonly DijkstraNextVertexResolver _dijkstraNextVertexResolver;
        readonly DijkstraVisitedVertexMarkingService _dijkstraVisitedVertexMarkingService;
        readonly DijkstraShouldConnectionReplaceCurrentFastestConnectionService 
            _dijkstraShouldConnectionReplaceCurrentFastestConnection;
        readonly DijkstraReplaceFastestConnectionService _dijkstraReplaceFastestConnectionService;
        readonly DijkstraStopConnectionsService _dijkstraStopConnectionsService;

        public DijkstraConnectionSearchEngine(
            DijkstraEmptyFastestConnectionsArrayFactory dijkstraEmptyFastestConnectionsArrayFactory,
            DijkstraNextVertexResolver dijkstraNextVertexResolver,
            DijkstraVisitedVertexMarkingService dijkstraVisitedVertexMarkingService,
            DijkstraShouldConnectionReplaceCurrentFastestConnectionService
                dijkstraShouldConnectionReplaceCurrentFastestConnection,
            DijkstraReplaceFastestConnectionService dijkstraReplaceFastestConnectionService,
            DijkstraStopConnectionsService dijkstraStopConnectionsService)
        {
            _dijkstraEmptyFastestConnectionsArrayFactory = dijkstraEmptyFastestConnectionsArrayFactory;
            _dijkstraNextVertexResolver = dijkstraNextVertexResolver;
            _dijkstraVisitedVertexMarkingService = dijkstraVisitedVertexMarkingService;
            _dijkstraShouldConnectionReplaceCurrentFastestConnection = 
                dijkstraShouldConnectionReplaceCurrentFastestConnection;
            _dijkstraReplaceFastestConnectionService = dijkstraReplaceFastestConnectionService;
            _dijkstraStopConnectionsService = dijkstraStopConnectionsService;
        }

        public IEnumerable<StopConnection> SearchConnections(SearchInput search, StopGraph graph)
        {
            var vertexFastestConnections = _dijkstraEmptyFastestConnectionsArrayFactory
                .Create(graph, search.StartStop, search.StartTime);
            var currentVertex = _dijkstraNextVertexResolver.GetFirstVertex(graph, search.StartStop);            
            while (currentVertex != null && currentVertex.Stop.Id != search.DestinationStop.Id)
            {
                foreach (var stopConnection in currentVertex.StopConnections)
                {
                    var destinationStopFastestConnection = _dijkstraStopConnectionsService
                        .GetDestinationStopFastestConnection(vertexFastestConnections, stopConnection);
                    var stopConnectionFromPreviousVertex = _dijkstraStopConnectionsService
                        .GetStopConnectionFromPreviousVertex(vertexFastestConnections, stopConnection);
                    if (_dijkstraShouldConnectionReplaceCurrentFastestConnection
                        .Return(search, stopConnectionFromPreviousVertex, 
                            destinationStopFastestConnection, stopConnection))
                    {
                        _dijkstraReplaceFastestConnectionService
                            .Replace(destinationStopFastestConnection, stopConnection);
                    }
                }
                _dijkstraVisitedVertexMarkingService.MarkAsVisited(currentVertex);
                currentVertex = _dijkstraNextVertexResolver.GetNextVertex(graph, vertexFastestConnections);
            }            
            return vertexFastestConnections;
        }
    }
}
