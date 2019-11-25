using Chilicki.Commline.Domain.Entities;
using System.Data.Entity;

namespace Chilicki.Commline.Infrastructure.Databases
{
    public class CommlineDBContext : DbContext
    {
        public CommlineDBContext() : base("Commline") {  }

        public virtual DbSet<Departure> Departures { get; set; }
        public virtual DbSet<Line> Lines { get; set; }
        public virtual DbSet<Trip> RouteStops { get; set; }
        public virtual DbSet<Stop> Stops { get; set; }
    }
}
