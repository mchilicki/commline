﻿using Chilicki.Commline.Domain.Search.Aggregates;
using Chilicki.Commline.Domain.Search.Aggregates.Graphs;
using Chilicki.Commline.Domain.Search.Factories.Dijkstra;
using Chilicki.Commline.Domain.Search.Services.Base;
using Chilicki.Commline.Domain.Search.Services.Dijkstra;
using System.Collections.Generic;

namespace Chilicki.Commline.Domain.Search.Services
{
    public class DijkstraConnectionSearchEngine : IConnectionSearchEngine
    {
        readonly DijkstraEmptyFastestConnectionsFactory _dijkstraEmptyFastestConnectionsFactory;
        readonly DijkstraNextVertexResolver _dijkstraNextVertexResolver;
        readonly DijkstraFastestConnectionReplacer _dijkstraFastestConnectionReplacer;
        readonly DijkstraStopConnectionsService _dijkstraStopConnectionsService;
        readonly DijkstraStopGraphService _dijkstraStopGraphService;

        public DijkstraConnectionSearchEngine(
            DijkstraEmptyFastestConnectionsFactory dijkstraEmptyFastestConnectionsArrayFactory,
            DijkstraNextVertexResolver dijkstraNextVertexResolver,
            DijkstraFastestConnectionReplacer dijkstraReplaceFastestConnectionService,
            DijkstraStopConnectionsService dijkstraStopConnectionsService,
            DijkstraStopGraphService dijkstraStopGraphService)
        {
            _dijkstraEmptyFastestConnectionsFactory = dijkstraEmptyFastestConnectionsArrayFactory;
            _dijkstraNextVertexResolver = dijkstraNextVertexResolver;
            _dijkstraFastestConnectionReplacer = dijkstraReplaceFastestConnectionService;
            _dijkstraStopConnectionsService = dijkstraStopConnectionsService;
            _dijkstraStopGraphService = dijkstraStopGraphService;
        }

        public IEnumerable<StopConnection> SearchConnections(SearchInput search, StopGraph graph)
        {
            var vertexFastestConnections = _dijkstraEmptyFastestConnectionsFactory
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
                    if (_dijkstraFastestConnectionReplacer
                        .ShouldConnectionBeReplaced(search, stopConnectionFromPreviousVertex, 
                            destinationStopFastestConnection, stopConnection))
                    {
                        _dijkstraFastestConnectionReplacer
                            .ReplaceWithNewFastestConnection(destinationStopFastestConnection, stopConnection);
                    }
                }
                _dijkstraStopGraphService.MarkVertexAsVisited(currentVertex);
                currentVertex = _dijkstraNextVertexResolver.GetNextVertex(graph, vertexFastestConnections);
            }            
            return vertexFastestConnections;
        }
    }
}
