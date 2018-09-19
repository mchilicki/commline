using Chilicki.Commline.Domain.Entities;
using Chilicki.Commline.Infrastructure.Repositories.Base;
using System.Data.Entity;

namespace Chilicki.Commline.Infrastructure.Repositories
{
    public class StopRepository : BaseRepository<Stop>
    {
        public StopRepository(DbContext db) : base(db)
        {
        }
    }
}
