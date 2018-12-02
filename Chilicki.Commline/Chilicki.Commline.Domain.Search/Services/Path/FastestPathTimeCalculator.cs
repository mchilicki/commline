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
            var travelStartTime = fastestPath.Path.First(p => p.IsTransfer == false).StartDateTime;
            var travelEndTime = fastestPath.Path.Last(p => p.IsTransfer == false).EndDateTime;
            return Math.Abs((travelEndTime - travelStartTime).Minutes);
        }

        public int CalculateConnectionTime(StopConnection waitingConnection)
        {
            var travelStartTime = waitingConnection.StartDateTime;
            var travelEndTime = waitingConnection.EndDateTime;
            return Math.Abs((travelEndTime - travelStartTime).Minutes);
        }
    }
}
