﻿using Chilicki.Commline.Domain.Entities;
using Chilicki.Commline.Infrastructure.Databases;
using Chilicki.Commline.Infrastructure.Repositories.Base;
using System.Collections.Generic;
using System.Linq;

namespace Chilicki.Commline.Infrastructure.Repositories
{
    public class StopRepository : BaseRepository<Stop>
    {
        public StopRepository(CommlineDBContext db) : base(db)
        {
        }

        public IEnumerable<Stop> GetAllNotConnectedToAnyLine()
        {
            return _dbSet
                .Where(e => !e.RouteStops.Any())
                .ToList();
        }

        public int GetStopNumberForStopName(string stopName)
        {
            return _dbSet
                .Where(e => e.Name == stopName)
                .Count() + 1;
        }
    }
}
