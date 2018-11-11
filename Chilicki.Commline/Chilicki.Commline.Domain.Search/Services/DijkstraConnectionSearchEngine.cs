using Chilicki.Commline.Domain.Search.Aggregates;
using Chilicki.Commline.Domain.Search.Aggregates.Graphs;
using Chilicki.Commline.Domain.Search.Factories.Dijkstra;
using Chilicki.Commline.Domain.Search.Services.Base;
using Chilicki.Commline.Domain.Search.Services.Dijkstra;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public DijkstraConnectionSearchEngine(
            DijkstraEmptyFastestConnectionsArrayFactory dijkstraEmptyFastestConnectionsArrayFactory,
            DijkstraNextVertexResolver dijkstraNextVertexResolver,
            DijkstraVisitedVertexMarkingService dijkstraVisitedVertexMarkingService,
            DijkstraShouldConnectionReplaceCurrentFastestConnectionService
                dijkstraShouldConnectionReplaceCurrentFastestConnection,
            DijkstraReplaceFastestConnectionService dijkstraReplaceFastestConnectionService)
        {
            _dijkstraEmptyFastestConnectionsArrayFactory = dijkstraEmptyFastestConnectionsArrayFactory;
            _dijkstraNextVertexResolver = dijkstraNextVertexResolver;
            _dijkstraVisitedVertexMarkingService = dijkstraVisitedVertexMarkingService;
            _dijkstraShouldConnectionReplaceCurrentFastestConnection = 
                dijkstraShouldConnectionReplaceCurrentFastestConnection;
            _dijkstraReplaceFastestConnectionService = dijkstraReplaceFastestConnectionService;
        }

        public IEnumerable<StopConnection> SearchConnections(SearchInput search, StopGraph graph)
        {
            var vertexFastestConnections = _dijkstraEmptyFastestConnectionsArrayFactory
                .Create(graph, search.StartStop, search.StartTime);
            var currentVertex = _dijkstraNextVertexResolver.GetFirstVertex(graph, search.StartStop);            
            while (currentVertex != null)
            {
                // TODO WSIADANIE DO AUTOBUSU KTORY JUZ ODJECHAL
                foreach (var stopConnection in currentVertex.StopConnections)
                {
                    var destinationStopFastestConnection = vertexFastestConnections
                        .First(p => p.DestinationStop.Stop.Id == stopConnection.DestinationStop.Stop.Id);
                    if (_dijkstraShouldConnectionReplaceCurrentFastestConnection
                        .Return(search, destinationStopFastestConnection, stopConnection))
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
