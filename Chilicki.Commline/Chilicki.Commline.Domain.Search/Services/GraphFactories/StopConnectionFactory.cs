using Chilicki.Commline.Domain.Entities;
using Chilicki.Commline.Domain.Search.Aggregates.Graphs;
using System.Collections.Generic;
using System.Linq;

namespace Chilicki.Commline.Domain.Search.Services.GraphFactories
{
    public class StopConnectionFactory
    {
        public StopConnection Create(
            RouteStop routeStop, 
            Departure departure,
            StopVertex currentVertex,
            RouteStop nextRouteStop,
            StopVertex nextVertex)
        {
            return new StopConnection()
            {
                Line = routeStop.Line,
                StartTime = departure.DepartureTime,
                SourceStop = currentVertex,
                EndTime = nextRouteStop
                                    .Departures
                                    .Where(p => p.RunIndex == departure.RunIndex)
                                    .First()
                                    .DepartureTime,
                DestinationStop = nextVertex,
            };
        }
    }
}
