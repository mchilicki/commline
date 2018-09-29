using Chilicki.Commline.Domain.Entities;
using Chilicki.Commline.Infrastructure.Databases;
using Chilicki.Commline.Infrastructure.Repositories.Base;

namespace Chilicki.Commline.Infrastructure.Repositories
{
    public class StopRepository : BaseRepository<Stop>
    {
        public StopRepository(CommlineDBContext db) : base(db)
        {
        }

    }
}
