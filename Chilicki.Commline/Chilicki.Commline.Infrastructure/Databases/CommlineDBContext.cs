using Chilicki.Commline.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Chilicki.Commline.Infrastructure.Databases
{
    public class CommlineDBContext : DbContext
    {
        public CommlineDBContext() : base() {  }

        public virtual DbSet<Departure> Departures { get; set; }
        public virtual DbSet<Line> Lines { get; set; }
        public virtual DbSet<RouteStop> RouteStops { get; set; }
        public virtual DbSet<Stop> Stops { get; set; }
    }
}
