using Chilicki.Commline.Domain.Entities;
using Chilicki.Commline.Infrastructure.Repositories.Base;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Chilicki.Commline.Infrastructure.Repositories
{
    public class StopRepository : BaseRepository<Stop>
    {
        private RouteStationRepository _routeStationRepository;

        public StopRepository(DbContext db, RouteStationRepository routeStationRepository) : base(db)
        {
            _routeStationRepository = routeStationRepository;
        }

        public IEnumerable<Stop> GetAllForLineId(long lineId)
        {
            return _routeStationRepository.GetAll()
                .Where(r => r.Line != null && r.Line.Id == lineId)
                .OrderBy(r => r.RouteOrderId)
                .Select(r => r.Stop);          
        }
    }
}
