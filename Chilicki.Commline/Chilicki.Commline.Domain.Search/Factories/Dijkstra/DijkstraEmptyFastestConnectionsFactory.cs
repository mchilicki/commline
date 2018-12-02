using Chilicki.Commline.Domain.Entities;
using Chilicki.Commline.Domain.Search.Aggregates;
using Chilicki.Commline.Domain.Search.Aggregates.Graphs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chilicki.Commline.Domain.Search.Factories.Dijkstra
{
    public class DijkstraEmptyFastestConnectionsFactory
    {
        public IEnumerable<StopConnection> Create(StopGraph graph, SearchInput search)
        {
            var vertexFastestConnections = new List<StopConnection>();
            var startingVertex = graph
                    .StopVertices
                    .First(p => p.Stop.Id == search.StartStop.Id);
            var startingConnection = new StopConnection()
            {
                DestinationStop = startingVertex,
                SourceStop = startingVertex,
                Line = null,
                StartDateTime = search.StartFullDate,
                EndDateTime = search.StartFullDate,
            };
            vertexFastestConnections.Add(startingConnection);
            foreach (var vertex in graph.StopVertices)
            {
                if (vertex.Stop.Id != search.StartStop.Id)
                {
                    vertexFastestConnections.Add(new StopConnection()
                    {
                        DestinationStop = vertex,
                        SourceStop = null,
                        Line = null,
                        StartDateTime = DateTime.MinValue,
                        EndDateTime = DateTime.MinValue,
                    });
                }                
            }
            return vertexFastestConnections;
        }
    }
}
