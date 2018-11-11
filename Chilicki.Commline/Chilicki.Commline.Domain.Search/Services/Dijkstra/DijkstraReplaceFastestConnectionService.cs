using Chilicki.Commline.Domain.Search.Aggregates.Graphs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chilicki.Commline.Domain.Search.Services.Dijkstra
{
    public class DijkstraReplaceFastestConnectionService
    {
        public void Replace(
            StopConnection currentFastestStopConnection, 
            StopConnection newFastestConnection)
        {
            currentFastestStopConnection.Line = newFastestConnection.Line;
            currentFastestStopConnection.StartTime = newFastestConnection.StartTime;
            currentFastestStopConnection.SourceStop = newFastestConnection.SourceStop;
            currentFastestStopConnection.EndTime = newFastestConnection.EndTime;
        }
    }
}
