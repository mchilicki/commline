using Chilicki.Commline.Domain.Search.Aggregates.Graphs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chilicki.Commline.Domain.Search.Services.Dijkstra
{
    public class DijkstraVisitedVertexMarkingService
    {
        public StopVertex MarkAsVisited(StopVertex stopVertex)
        {
            stopVertex.IsVisited = true;
            return stopVertex;
        } 
    }
}
