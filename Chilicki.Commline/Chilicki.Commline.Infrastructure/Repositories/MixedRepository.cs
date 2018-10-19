using Chilicki.Commline.Domain.Entities;
using Chilicki.Commline.Infrastructure.Databases;
using System.Collections.Generic;
using System.Linq;

namespace Chilicki.Commline.Infrastructure.Repositories
{
    public class MixedRepository
    {
        CommlineDBContext _db;

        public MixedRepository(CommlineDBContext db)
        {
            _db = db;
        }

        public IEnumerable<Stop> GetAllStopsForLineId(long lineId)
        {
            return _db.RouteStops
                .Where(r => r.Line != null && r.Line.Id == lineId)
                .OrderBy(r => r.StopIndex)
                .Select(r => r.Stop);
        }
    }
}
