using Chilicki.Commline.Domain.Entities;
using Chilicki.Commline.Infrastructure.Repositories.Base;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Chilicki.Commline.Infrastructure.Repositories
{
    public class StopRepository : BaseRepository<Stop>
    {
        private RouteStopRepository _RouteStopRepository;

        public StopRepository(DbContext db, RouteStopRepository RouteStopRepository) : base(db)
        {
            _RouteStopRepository = RouteStopRepository;
        }

        public IEnumerable<Stop> GetAllForLineId(long lineId)
        {
            return _RouteStopRepository.GetAll()
                .Where(r => r.Line != null && r.Line.Id == lineId)
                .OrderBy(r => r.StopIndex)
                .Select(r => r.Stop);          
        }
    }
}
