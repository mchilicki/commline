using Chilicki.Commline.Common.Extensions;
using Chilicki.Commline.Domain.Entities;
using System.Linq;

namespace Chilicki.Commline.Domain.Services.Routes
{
    public class NextRouteStopResolver
    {
        public RouteStop GetNextRouteStop(RouteStop routeStop)
        {
            var lineRouteStops = routeStop.Line.RouteStops;
            return lineRouteStops
                .Where(p => p.StopIndex == routeStop.StopIndex + 1)
                .FirstOrNull();
        }
    }
}
