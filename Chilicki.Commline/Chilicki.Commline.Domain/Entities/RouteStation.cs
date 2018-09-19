using System.Collections.Generic;

namespace Chilicki.Commline.Domain.Entities
{
    public class RouteStation
    {
        public long Id { get; set; }
        public int RouteOrderId { get; set; }
        public virtual ICollection<Departure> Departures { get; set; }
        public virtual Line Line { get; set; }
        public virtual Stop Stop { get; set; }
    }
}
