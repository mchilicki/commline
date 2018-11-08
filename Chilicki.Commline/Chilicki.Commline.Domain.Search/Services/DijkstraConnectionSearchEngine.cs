using Chilicki.Commline.Domain.Search.Aggregates;
using Chilicki.Commline.Domain.Search.Aggregates.Graphs;
using Chilicki.Commline.Domain.Search.Factories.Dijkstra;
using Chilicki.Commline.Domain.Search.Services.Base;
using Chilicki.Commline.Domain.Search.Services.Dijkstra;
using System;
using System.Collections.Generic;

namespace Chilicki.Commline.Domain.Search.Services
{
    public class DijkstraConnectionSearchEngine : IConnectionSearchEngine
    {
        readonly DijkstraEmptyFastestConnectionsArrayFactory _dijkstraEmptyFastestConnectionsArrayFactory;
        readonly DijkstraNextVertexResolver _dijkstraNextVertexResolver;
        readonly DijkstraVisitedVertexMarkingService _dijkstraVisitedVertexMarkingService;

        public DijkstraConnectionSearchEngine(
            DijkstraEmptyFastestConnectionsArrayFactory dijkstraEmptyFastestConnectionsArrayFactory,
            DijkstraNextVertexResolver dijkstraNextVertexResolver,
            DijkstraVisitedVertexMarkingService dijkstraVisitedVertexMarkingService)
        {
            _dijkstraEmptyFastestConnectionsArrayFactory = dijkstraEmptyFastestConnectionsArrayFactory;
            _dijkstraNextVertexResolver = dijkstraNextVertexResolver;
            _dijkstraVisitedVertexMarkingService = dijkstraVisitedVertexMarkingService;
        }

        public IEnumerable<StopConnection> SearchConnections(SearchInput search, StopGraph graph)
        {
            var vertexFastestConnections = _dijkstraEmptyFastestConnectionsArrayFactory.Create(graph, search.StartStop, search.StartTime);
            var currentVertex = _dijkstraNextVertexResolver.GetFirstVertex(graph, search.StartStop);
            _dijkstraVisitedVertexMarkingService.MarkAsVisited(currentVertex);

            return vertexFastestConnections;
        }
    }
}
