using Chilicki.Commline.Domain.Entities;
using Chilicki.Commline.Infrastructure.Repositories.Base;
using System.Data.Entity;

namespace Chilicki.Commline.Infrastructure.Repositories
{
    public class DepartureRepository : BaseRepository<Departure>
    {
        public DepartureRepository(DbContext db) : base(db)
        {

        }
    }
}
