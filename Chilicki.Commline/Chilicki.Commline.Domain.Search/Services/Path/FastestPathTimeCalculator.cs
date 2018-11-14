using Chilicki.Commline.Domain.Search.Aggregates;
using Chilicki.Commline.Domain.Search.Aggregates.Graphs;
using System;
using System.Linq;

namespace Chilicki.Commline.Domain.Search.Services.Path
{
    public class FastestPathTimeCalculator
    {
        public int CalculateTravelTime(FastestPath fastestPath)
        {
            var travelStartTime = fastestPath.Path.First(p => p.IsTransfer == false).StartTime;
            var travelEndTime = fastestPath.Path.Last(p => p.IsTransfer == false).EndTime;
            return Math.Abs((travelEndTime - travelStartTime).Minutes);
        }

        public int CalculateConnectionTime(StopConnection waitingConnection)
        {
            var travelStartTime = waitingConnection.StartTime;
            var travelEndTime = waitingConnection.EndTime;
            return Math.Abs((travelEndTime - travelStartTime).Minutes);
        }
    }
}
