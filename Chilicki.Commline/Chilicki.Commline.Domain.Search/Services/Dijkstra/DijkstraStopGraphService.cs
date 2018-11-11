using Chilicki.Commline.Domain.Entities;
using Chilicki.Commline.Domain.Search.Aggregates.Graphs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chilicki.Commline.Domain.Search.Services.Dijkstra
{
    public class DijkstraStopGraphService
    {
        public StopVertex GetStopVertexByStop(StopGraph graph, Stop stop)
        {
            return graph
                .StopVertices
                .First(p => p.Stop.Id == stop.Id);
        }
    }
}
