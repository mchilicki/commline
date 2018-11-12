using Chilicki.Commline.Domain.Entities;
using Chilicki.Commline.Domain.Search.Aggregates.Graphs;
using Chilicki.Commline.Domain.Search.Services.GraphFactories.Base;
using Chilicki.Commline.Domain.Services.Routes;
using System.Collections.Generic;
using System.Linq;

namespace Chilicki.Commline.Domain.Search.Services.GraphFactories
{
    public class StopGraphFactory : IGraphFactory<StopGraph>
    {
        readonly NextRouteStopResolver _routeService;
        readonly StopConnectionFactory _stopConnectionFactory;

        public StopGraphFactory(
            NextRouteStopResolver routeService,
            StopConnectionFactory stopConnectionFactory)
        {
            _routeService = routeService;
            _stopConnectionFactory = stopConnectionFactory;
        }

        public StopGraph CreateGraph(IEnumerable<Stop> stops)
        {
            var stopVertices = GenerateEmptyStopVertices(stops);
            stopVertices = FillStopVerticesWithStopConnections(stopVertices, stops);
            return new StopGraph()
            {
                StopVertices = stopVertices,
            };
        }

        private IEnumerable<StopVertex> GenerateEmptyStopVertices(IEnumerable<Stop> stops)
        {
            var stopVertices = new List<StopVertex>();
            foreach (var stop in stops)
            {
                stopVertices.Add(new StopVertex()
                {
                    Stop = stop,
                    StopConnections = new List<StopConnection>(),
                    IsVisited = false,
                });
            }
            return stopVertices;
        }

        private IEnumerable<StopVertex> FillStopVerticesWithStopConnections
            (IEnumerable<StopVertex> allStopVertices, IEnumerable<Stop> stops)
        {
            foreach (var vertex in allStopVertices)
            {
                var stopConnections = new List<StopConnection>();
                foreach (var routeStop in vertex.Stop.RouteStops)
                {
                    var nextRouteStop = _routeService.GetNextRouteStop(routeStop);
                    if (nextRouteStop != null)
                    {
                        var nextVertex = allStopVertices
                                    .Where(p => p.Stop.Id == nextRouteStop.Stop.Id)
                                    .First();
                        foreach (var departure in routeStop.Departures)
                        {
                            stopConnections.Add(_stopConnectionFactory.Create(
                                routeStop, departure, vertex, nextRouteStop, nextVertex));
                        }
                    }
                }
                vertex.StopConnections = stopConnections;
            }
            return allStopVertices;
        }
    }
}