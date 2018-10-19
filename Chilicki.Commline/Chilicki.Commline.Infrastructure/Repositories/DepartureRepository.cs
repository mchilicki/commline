using Chilicki.Commline.Domain.Entities;
using Chilicki.Commline.Infrastructure.Databases;
using Chilicki.Commline.Infrastructure.Repositories.Base;

namespace Chilicki.Commline.Infrastructure.Repositories
{
    public class DepartureRepository : BaseRepository<Departure>
    {
        public DepartureRepository(CommlineDBContext db) : base(db)
        {

        }
    }
}
