using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Chilicki.Commline.Domain.Entities
{
    public class Stop
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int StopNumber { get; set; }
        public virtual ICollection<RouteStop> RouteStops { get; set; }
    }
}
