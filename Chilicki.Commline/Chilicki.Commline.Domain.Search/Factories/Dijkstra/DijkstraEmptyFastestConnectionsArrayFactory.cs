using Chilicki.Commline.Domain.Entities;
using Chilicki.Commline.Domain.Search.Aggregates.Graphs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chilicki.Commline.Domain.Search.Factories.Dijkstra
{
    public class DijkstraEmptyFastestConnectionsArrayFactory
    {
        public IEnumerable<StopConnection> Create(StopGraph graph, Stop startingStop, TimeSpan startTime)
        {
            var vertexFastestConnections = new List<StopConnection>();
            var startingVertex = graph
                    .StopVertices
                    .First(p => p.Stop.Id == startingStop.Id);
            var startingConnection = new StopConnection()
            {
                DestinationStop = startingVertex,
                SourceStop = startingVertex,
                Line = null,
                StartTime = startTime,
                EndTime = startTime,
            };
            vertexFastestConnections.Add(startingConnection);
            foreach (var vertex in graph.StopVertices)
            {
                if (vertex.Stop.Id != startingStop.Id)
                {
                    vertexFastestConnections.Add(new StopConnection()
                    {
                        DestinationStop = vertex,
                        SourceStop = null,
                        Line = null,
                        StartTime = TimeSpan.Zero,
                        EndTime = TimeSpan.Zero,
                    });
                }                
            }
            return vertexFastestConnections;
        }
    }
}
