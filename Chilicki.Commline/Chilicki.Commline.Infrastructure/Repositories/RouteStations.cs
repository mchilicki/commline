using Chilicki.Commline.Domain.Entities;
using Chilicki.Commline.Infrastructure.Repositories.Base;
using System.Data.Entity;

namespace Chilicki.Commline.Infrastructure.Repositories
{
    public class RouteStationRepository : BaseRepository<RouteStation>
    {
        public RouteStationRepository(DbContext db) : base(db)
        {

        }
    }
}
