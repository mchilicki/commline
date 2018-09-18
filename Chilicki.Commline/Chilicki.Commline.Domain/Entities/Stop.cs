using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chilicki.Commline.Domain.Entities
{
    public class Stop
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int SiteNumber { get; set; }
        public virtual ICollection<RouteStation> RouteStations { get; set; }
    }
}
