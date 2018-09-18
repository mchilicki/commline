using Chilicki.Commline.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chilicki.Commline.Infrastructure.Databases
{
    public class CommlineDBContext : DbContext
    {
        public CommlineDBContext() : base() { }

        public virtual DbSet<Departure> Departures { get; set; }
        public virtual DbSet<Line> Lines { get; set; }
        public virtual DbSet<RouteStation> RouteStations { get; set; }
        public virtual DbSet<Stop> Stops { get; set; }
    }
}
