using Chilicki.Commline.Common.Extensions;
using Chilicki.Commline.Domain.Entities;
using Chilicki.Commline.Domain.Search.Aggregates.Graphs;
using Chilicki.Commline.Domain.Search.Services.GraphFactories.Base;
using Chilicki.Commline.Domain.Services.Routes;
using System;
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

        public StopGraph CreateGraph(IEnumerable<Stop> stops, DateTime day)
        {
            var stopVertices = GenerateEmptyStopVertices(stops);
            stopVertices = FillStopVerticesWithSimilarStopVertices(stopVertices, stops);
            stopVertices = FillStopVerticesWithStopConnections(stopVertices, stops, day);
            stopVertices = FillStopVerticesWithStopConnections(stopVertices, stops, day.AddDays(1));
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
            (IEnumerable<StopVertex> allStopVertices, IEnumerable<Stop> stops, DateTime connectionDay)
        {
            foreach (var vertex in allStopVertices)
            {
                var stopConnections = vertex.StopConnections.ToList();
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
                            if (departure.IsOnNextDay)
                            {
                                stopConnections.Add(_stopConnectionFactory.Create(
                                    routeStop, departure, vertex, nextRouteStop, nextVertex, connectionDay, betweenTwoDays: true));
                                //stopConnections.Add(_stopConnectionFactory.Create(
                                //    routeStop, departure, vertex, nextRouteStop, nextVertex, connectionDay.AddDays(1), betweenTwoDays: true));                                
                            }
                            else
                            {
                                stopConnections.Add(_stopConnectionFactory.Create(
                                    routeStop, departure, vertex, nextRouteStop, nextVertex, connectionDay, betweenTwoDays: false));
                            }                            
                        }
                    }
                }
                vertex.StopConnections = stopConnections;
            }
            return allStopVertices;
        }

        private IEnumerable<StopVertex> FillStopVerticesWithSimilarStopVertices
            (IEnumerable<StopVertex> allStopVertices, IEnumerable<Stop> stops)
        {
            foreach(var vertex in allStopVertices)
            {
                var similarVertices = new List<StopVertex>();
                var sameStops = stops
                    .Where(p => p.Name == vertex.Stop.Name &&
                        p.Id != vertex.Stop.Id);
                foreach (var sameStop in sameStops)
                {
                    var similarVertex = allStopVertices
                        .FirstOrNull(p => p.Stop.Id == sameStop.Id);
                    if (similarVertex != null)
                    {
                        similarVertices.Add(similarVertex);
                    }
                }
                vertex.SimilarStopVertices = similarVertices;
            }
            return allStopVertices;
        }
    }
}