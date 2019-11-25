using Chilicki.Commline.Common.Extensions;
using Chilicki.Commline.Domain.Entities;
using System.Linq;

namespace Chilicki.Commline.Domain.Services.Routes
{
    public class NextRouteStopResolver
    {
        public Trip GetNextRouteStop(Trip routeStop)
        {
            var lineRouteStops = routeStop.Line.Trips;
            return lineRouteStops
                .Where(p => p.StopIndex == routeStop.StopIndex + 1)
                .FirstOrNull();
        }
    }
}
