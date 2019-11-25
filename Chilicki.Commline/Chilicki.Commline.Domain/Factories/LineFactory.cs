using Chilicki.Commline.Domain.Entities;
using Chilicki.Commline.Domain.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Chilicki.Commline.Domain.Factories
{
    public class LineFactory
    {
        public Line FillIn(Line line, string name, string color, 
            bool IsCircular, LineType lineType, IEnumerable<Trip> routeStops)
        {
            line.Name = name;
            line.Color = color;
            line.IsCircular = IsCircular;
            line.LineType = lineType;
            line.Trips = routeStops.ToList();
            return line;
        }
    }
}
