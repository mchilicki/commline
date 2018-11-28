using Chilicki.Commline.Domain.Entities;
using Chilicki.Commline.Domain.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Chilicki.Commline.Domain.Factories
{
    public class LineFactory
    {
        public Line FillIn(Line line, string name, string color, 
            bool IsCircular, LineType lineType, IEnumerable<RouteStop> routeStops)
        {
            line.Name = name;
            line.Color = color;
            line.IsCircular = IsCircular;
            line.LineType = lineType;
            line.RouteStops = routeStops.ToList();
            return line;
        }
    }
}
