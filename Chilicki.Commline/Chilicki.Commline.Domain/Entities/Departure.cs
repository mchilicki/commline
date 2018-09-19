using Chilicki.Commline.Domain.Enums;
using System;

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
