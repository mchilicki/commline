using System.Collections.Generic;

namespace Chilicki.Commline.Domain.Entities
{
    public class Line
    {
        public long Id { get; set; }
        public string Number { get; set; }
        public virtual LineType LineType { get; set; }
        public virtual ICollection<RouteStation> RouteStations { get; set; }
    }
}
