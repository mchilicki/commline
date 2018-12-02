using Chilicki.Commline.Domain.Entities;
using Chilicki.Commline.Domain.Search.Aggregates.Graphs;
using System;
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
            StopVertex nextVertex,
            DateTime connectionDay)
        {
            return new StopConnection()
            {
                Line = routeStop.Line,
                StartDateTime = connectionDay + departure.DepartureTime,
                SourceStop = currentVertex,
                EndDateTime = connectionDay + nextRouteStop
                            .Departures
                            .Where(p => p.RunIndex == departure.RunIndex)
                            .First()
                            .DepartureTime,
                DestinationStop = nextVertex,
            };
        }
    }
}
