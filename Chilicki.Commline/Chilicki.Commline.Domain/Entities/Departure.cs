using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chilicki.Commline.Domain.Entities
{
    public class Departure
    {
        public long Id { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public virtual DayType DayType { get; set; }
        public virtual RouteStation RouteStation { get; set; }
    }
}
